using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;

namespace ICP_Algorithm.WFA.Methods
{
    internal class V_Shape_ICP
    {
        #region Parametes

        /// <summary>
        /// Lenght of the each arm in V shape object (Meters)
        /// </summary>
        internal double ArmLength = 50;

        /// <summary>
        /// Angle beetween arms (degree)
        /// </summary>
        internal double V_Angle = 150;

        /// <summary>
        /// Resolution of grid (meters)
        /// </summary>
        internal double GridResolution = 0.2;

        /// <summary>
        /// Angle of V object respect to origin
        /// </summary>
        internal double AngleOfV_Obj = 0;

        /// <summary>
        /// Position of V object respect to origin
        /// </summary>
        internal double[] PositionOfV_Obj = new double[] { 0, 0 };

        #endregion

        #region Private Variables
        private Matrix<double> ReferenceObject;

        /// will be used to build a matrix
        MatrixBuilder<double> Double_M_Builder = Matrix<double>.Build;
        #endregion

        /// <summary>
        /// This function Creates the V-Shape object using parameters above. 
        /// Created object will initially be in the origin with angle of zero.
        /// </summary>
        internal Matrix<double> CreateRefMatrix()
        {
            /// equation of a line is defined as y=ax+c
            /// this is the equation of the line which to be used to generate object points if x value is smaller than zero.
            /// this will be defined as a double array where the first element is "a" parameter and second element is "c" parameter of the line equation above
            double[] equation1;

            ///  similar to equation1 this is the equation of the line which to be used to generate object points if x value is greater than zero.
            /// this will be defined as a double array where the first element is "a" parameter and second element is "c" parameter of the line equation above
            double[] equation2;

            double v_AngleInRad = (180 - V_Angle) * Math.PI / 180.0;

            /// Calculate equations
            equation1 = new double[] { Math.Tan(v_AngleInRad / 2), 0 };
            equation2 = new double[] { Math.Tan(-v_AngleInRad / 2), 0 };

            /// Since the arms are symmetrical, we can take one of a parameters and calculate max x value according to arm length
            double projectionOfX = ArmLength * Math.Abs(Math.Cos(v_AngleInRad / 2));

            int numberOfPointsInV_Object = (int)(projectionOfX / GridResolution);

            ///Define reference object which will have 2 time numberOfPointsInV_Object +1.
            var referenceObject = Double_M_Builder.Dense(2, numberOfPointsInV_Object * 2 + 1);

            int pointIndex = 0;
            /// Calculate Points
            for (int i = -numberOfPointsInV_Object; i <= numberOfPointsInV_Object; i++)
            {
                referenceObject[0, pointIndex] = i * GridResolution;
                if (i < 0)
                    referenceObject[1, pointIndex] = referenceObject[0, pointIndex] * equation1[0] + equation1[1];
                else
                    referenceObject[1, pointIndex] = referenceObject[0, pointIndex] * equation2[0] + equation2[1];
                pointIndex++;
            }

            /// Now We can rotate and translate the object according to parameters

            ReferenceObject = ICP.Transform(referenceObject, AngleOfV_Obj, PositionOfV_Obj);

            //double AngleOfV_Obj_rad = AngleOfV_Obj * Math.PI / 180.0;

            ///// Lets create our rotation matrix
            //Matrix<double> R = Double_M_Builder.Dense(2, 2);
            //R[0, 0] = Math.Cos(AngleOfV_Obj_rad);
            //R[0, 1] = -Math.Sin(AngleOfV_Obj_rad);
            //R[1, 0] = Math.Sin(AngleOfV_Obj_rad);
            //R[1, 1] = Math.Cos(AngleOfV_Obj_rad);

            ///// Rotate the object
            //var referenceObject_rotated = R.Multiply(referenceObject);

            ///// Lets Create our translation Matrix;
            //Matrix<double> t = Double_M_Builder.Dense(2, 1);
            //t[0, 0] = PositionOfV_Obj[0];
            //t[1, 0] = PositionOfV_Obj[1];

            //ReferenceObject = referenceObject_rotated.Clone();
            ///// Translate the object
            //ReferenceObject.SetRow(0, referenceObject_rotated.Row(0).Add(t[0, 0]));
            //ReferenceObject.SetRow(1, referenceObject_rotated.Row(1).Add(t[1, 0]));

            return ReferenceObject.Clone();
        }

        internal List<double[]> FindVShapePoints(double[] posX, double[] posY)
        {
            int max_itr = 500;
            double threshold = 0.000001;

            var sampleData = WritePointsToMatrix(posX, posY);
            //if (ReferenceObject == null)
            CreateRefMatrix();

            var transformationMatrix = ICP.ICP_run(ReferenceObject, sampleData, threshold, max_itr, false);

            var R = transformationMatrix.SubMatrix(0, 2, 0, 2);
            var t = transformationMatrix.SubMatrix(0, 2, 2, 1);
            var offset = transformationMatrix.SubMatrix(0, 2, 3, 1);

            /// corner points coordinates will be stored in this variable. each row is a point in order of left corner, middle corner and right corner
            List<double[]> cornerPoints = new List<double[]>();

            /// Calculate inverse of rotation matrix which equals to transpose of it.
            var r_inv = R.Transpose();

            var transformedP_orig = ICP.AddVectorValsToMatrix(ReferenceObject, t.Multiply(-1));
            var transformedP = ICP.AddVectorValsToMatrix(r_inv.Multiply(transformedP_orig), offset.Multiply(1));

            var leftCorner = transformedP.Column(0).ToArray();
            cornerPoints.Add(leftCorner);

            var middleCorner = transformedP.Column(transformedP.ColumnCount / 2).ToArray();
            cornerPoints.Add(middleCorner);

            var rightCorner = transformedP.Column(transformedP.ColumnCount - 1).ToArray();
            cornerPoints.Add(rightCorner);

            return cornerPoints;
        }

        internal Matrix<double> FindVShapePoints(Matrix<double> sampleData)
        {
            int max_itr = 500;
            double threshold = 0.0000000001;

            //if (ReferenceObject == null)
            CreateRefMatrix();

            var transformationMatrix = ICP.ICP_run(ReferenceObject, sampleData, threshold, max_itr, false);

            var R = transformationMatrix.SubMatrix(0, 2, 0, 2);
            var t = transformationMatrix.SubMatrix(0, 2, 2, 1);
            var offset = transformationMatrix.SubMatrix(0, 2, 3, 1);

            /// corner points coordinates will be stored in this variable. each row is a point in order of left corner, middle corner and right corner
            List<double[]> cornerPoints = new List<double[]>();

            /// Calculate inverse of rotation matrix which equals to transpose of it.
            var r_inv = R.Transpose();

            var transformedP_orig = ICP.AddVectorValsToMatrix(ReferenceObject, t.Multiply(-1));
            var transformedP = ICP.AddVectorValsToMatrix(r_inv.Multiply(transformedP_orig), offset.Multiply(1));

            var leftCorner = transformedP.Column(0).ToArray();
            cornerPoints.Add(leftCorner);

            var middleCorner = transformedP.Column(transformedP.ColumnCount / 2).ToArray();
            cornerPoints.Add(middleCorner);

            var rightCorner = transformedP.Column(transformedP.ColumnCount - 1).ToArray();
            cornerPoints.Add(rightCorner);

            return transformedP;
        }

        internal static Matrix<double> WritePointsToMatrix(double[] posX, double[] posY)
        {
            int pointCount = posX.Length;
            var m = Matrix<double>.Build;
            var mat = m.Dense(2, pointCount);
            mat.SetRow(0, posX);
            mat.SetRow(1, posY);
            return mat;
        }
    }
}