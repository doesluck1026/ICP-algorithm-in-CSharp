/// ===============================
/// AUTHOR       :Yahya Sevikoglu - yahyasevikoglu@gmail.com
/// CREATE DATE  :29.11.2019
/// PURPOSE      : This class is used for calculation of convergens between two point clouds. These point clouds should be same dimensional but can have different number of sample points.
///                
/// NOTES:This class is based on Rigid Registration Iterative Closest Point Tutorial which can be found in http://www.sci.utah.edu/~shireen/pdfs/tutorials/Elhabian_ICP09.pdf.
/// This also uses Supercluster KD-Tree algorithm by Eric Regina.
/// ===============================
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;
using Supercluster.KDTree;

namespace icp_deneme
{
    class ICP
    {
        /// <summary>
        ///  Calculates Rotation, translation and error between two data sets and returns a Tuple of R,t,err.
        /// This Function should be called in a loop by updating P_points
        /// </summary>
        /// <param name="M_points"></param>
        /// <param name="P_points"></param>
        /// <param name="_3D"></param>
        /// <returns></returns>
        public static Tuple<Matrix<double>, Matrix<double>, double> ICP_run(Matrix<double> M_points, Matrix<double> P_points, bool _3D)
        {
            int Np;
            var m = Matrix<double>.Build;
            Np = P_points.ColumnCount;
            Matrix<double> Y;
            Y = KD_tree(M_points, P_points);
            double s = 1;

            Matrix<double> R;
            Matrix<double> t ;
            Vector<double> d;
            double err = 0;
            Matrix<double> dummy_Row = m.Dense(1, Np, 0);
            ///Nokta sayilari

            ///P ve Y matrislerinin agirlik merkezi hesaplaniyor
            Matrix<double> Mu_p = FindCentroid(P_points);
            Matrix<double> Mu_y = FindCentroid(Y);

            Matrix<double> dummy_p1 = m.Dense(1, Np);
            Matrix<double> dummy_p2 = m.Dense(1, Np);
            Matrix<double> dummy_p3 = m.Dense(1, Np, 0);
            Matrix<double> dummy_y1 = m.Dense(1, Np);
            Matrix<double> dummy_y2 = m.Dense(1, Np);
            Matrix<double> dummy_y3 = m.Dense(1, Np, 0);
            ///P matrisinin X ve Y koordinatlarini iceren satirlari farkli matrislere aliniyor
            dummy_p1.SetRow(0, P_points.Row(0));
            dummy_p2.SetRow(0, P_points.Row(1));
            if (_3D)
            {
                dummy_p3.SetRow(0, P_points.Row(2));
            }
            /// P deki her bir noktadan p nin agirlik merkezinin koordinatlari cikartiliyor(ZERO MEAN) yeni bir matris icerisine kaydediliyor.
            Matrix<double> P_prime = (dummy_p1 - Mu_p[0, 0]).Stack(dummy_p2 - Mu_p[1, 0]).Stack(dummy_p3 - Mu_p[2, 0]) ;
            ///Y matrisinin X ve Y koordinatlarini iceren satirlari farkli matrislere aliniyor
            dummy_y1.SetRow(0, Y.Row(0));
            dummy_y2.SetRow(0, Y.Row(1));
            if (_3D)
            {
                dummy_y3.SetRow(0, Y.Row(2));
            }
                /// P deki her bir noktadan p nin agirlik merkezinin koordinatlari cikartiliyor(ZERO MEAN) yeni bir matris icerisine kaydediliyor.
                Matrix<double> Y_prime = (dummy_y1 - Mu_y[0, 0]).Stack((dummy_y2 - Mu_y[1, 0]).Stack(dummy_y3 - Mu_y[2, 0]));
            /// -X -Y -Z koordinat matrisleri aliniyor.
            Matrix<double> Px = m.Dense(1, Np);
            Matrix<double> Py = m.Dense(1, Np);
            Matrix<double> Pz = m.Dense(1, Np, 0);
            Matrix<double> Yx = m.Dense(1, Np);
            Matrix<double> Yy = m.Dense(1, Np);
            Matrix<double> Yz = m.Dense(1, Np, 0);
            Px.SetRow(0, P_prime.Row(0));
            Py.SetRow(0, P_prime.Row(1));
            
            Yx.SetRow(0, Y_prime.Row(0));
            Yy.SetRow(0, Y_prime.Row(1));
            
            if (_3D)
            {
                Pz.SetRow(0, P_prime.Row(2));
                Yz.SetRow(0, Y_prime.Row(2));
            }

            var Sxx = Px * Yx.Transpose();
            var Sxy = Px * Yy.Transpose();
            var Sxz = Px * Yz.Transpose();

            var Syx = Py * Yx.Transpose();
            var Syy = Py * Yy.Transpose();
            var Syz = Py * Yz.Transpose();

            var Szx = Pz * Yx.Transpose();
            var Szy = Pz * Yy.Transpose();
            var Szz = Pz * Yz.Transpose();
            Matrix<double> Nmatrix = m.DenseOfArray(new[,]{{ Sxx[0, 0] + Syy[0, 0] + Szz[0, 0],  Syz[0, 0] - Szy[0, 0],       -Sxz[0, 0] + Szx[0, 0],        Sxy[0, 0] - Syx[0, 0]},
                                                {-Szy[0, 0] + Syz[0, 0],        Sxx[0, 0] - Syy[0, 0] - Szz[0, 0],  Sxy[0, 0] + Syx[0, 0],        Sxz[0, 0] + Szx[0, 0]},
                                                {Szx[0, 0] - Sxz[0, 0],         Syx[0, 0] + Sxy[0, 0],       -Sxx[0, 0] + Syy[0, 0] - Szz[0, 0],  Syz[0, 0] + Szy[0, 0]},
                                                {-Syx[0, 0] + Sxy[0, 0],        Szx[0, 0] + Sxz[0, 0],        Szy[0, 0] + Syz[0, 0],       -Sxx[0, 0] + Szz[0, 0] - Syy[0, 0]} });
            
            var evd = Nmatrix.Evd();
            Matrix<double> eigenvectors = evd.EigenVectors;
            var q = eigenvectors.Column(3);
            var q0 = q[0]; var q1 = q[1]; var q2 = q[2]; var q3 = q[3];

            ///Quernion matrislerinin bulunmasi
            var Qbar = m.DenseOfArray(new[,] { { q0, -q1, -q2, -q3 },
                                               { q1, q0, q3, -q2 },
                                               { q2, -q3, q0, q1 },
                                               { q3, q2, -q1, q0 }});

            var Q = m.DenseOfArray(new[,] {    { q0, -q1, -q2, -q3 },
                                               { q1, q0, -q3, q2 },
                                               { q2, q3, q0, -q1 },
                                               { q3, -q2, q1, q0 }});
            ///Rotasyon matrisi hesabi
            R = (Qbar.Transpose()).Multiply(Q);
            R = (R.RemoveColumn(0)).RemoveRow(0);
           
            ///Translation hesabi
            t = Mu_y - s * R * Mu_p;

            ///hata hesabi     
            if (!_3D)
            {
                Matrix<double> pp = P_points.Stack(dummy_Row);

                for (int i = 0; i < Np; i++)
                {
                    d = Y.Column(i).Subtract(pp.Column(i));
                    err += d[0] * d[0] + d[1] * d[1] + d[2] * d[2];////
                }
            }
            else
            {
                for (int i = 0; i < Np; i++)
                {
                    d = Y.Column(i).Subtract(P_points.Column(i));
                    err += d[0] * d[0] + d[1] * d[1] + d[2] * d[2];
                }
            }
            Tuple<Matrix<double>, Matrix<double>, double> ret = new Tuple<Matrix<double>, Matrix<double>, double>(R,t, err);
            return ret;
        }
        
        /// <summary>
        /// This Function Calculates Final P_points under the conditions of either threshold or maximum iterations that given as input.
        /// Returns matched P_points.
        /// </summary>
        /// <param name="M_points"></param>
        /// <param name="P_points"></param>
        /// <param name="threshold"></param>
        /// <param name="max_iterations"></param>
        /// <param name="_3D"></param>
        /// <returns></returns>
        public static Matrix<double> ICP_run(Matrix<double> M_points, Matrix<double> P_points,double threshold,int max_iterations,bool _3D,ref Matrix<double> ref_vector)
        {
            #region "Definitions"
            var m = Matrix<double>.Build;
            int Np = P_points.ColumnCount;
            Matrix<double> dummy_p1 = m.Dense(1, Np);
            Matrix<double> dummy_p2 = m.Dense(1, Np);
            Matrix<double> dummy_p3 = m.Dense(1, Np, 0);
            Matrix<double> dummy_y1 = m.Dense(1, Np);
            Matrix<double> dummy_y2 = m.Dense(1, Np);
            Matrix<double> dummy_y3 = m.Dense(1, Np, 0);
            Matrix<double> Px = m.Dense(1, Np);
            Matrix<double> Py = m.Dense(1, Np);
            Matrix<double> Pz = m.Dense(1, Np, 0);
            Matrix<double> Yx = m.Dense(1, Np);
            Matrix<double> Yy = m.Dense(1, Np);
            Matrix<double> Yz = m.Dense(1, Np, 0);
            Matrix<double> dummy_Row = m.Dense(1, Np, 0);
            Matrix<double> Third_raw = m.Dense(1, Np, 0); ///dummy row to be used in calculations
            Matrix<double> Px2 = m.Dense(1, Np);
            Matrix<double> Py2 = m.Dense(1, Np);
            Matrix<double> Pz2 = m.Dense(1, Np);
            Matrix<double> P_points2;
            double s = 1;
            double delta_error_thresh = 0.00001;
            double previous_error = 0;

            Matrix<double> Third_raw1 = m.Dense(1, 1, 0); ///dummy row to be used in calculations

            Matrix<double> R ;
            Matrix<double> t ;
            double err = 0;
            Matrix<double> Y;
            Vector<double> d;
            #endregion
            for (int itr = 1; itr <= max_iterations; itr++)
            {
                Y = KD_tree(M_points, P_points);
                ///Calculate Centroid for both point clouds
                ///P ve Y matrislerinin agirlik merkezi hesaplaniyor
                Matrix<double> Mu_p = FindCentroid(P_points);
                Matrix<double> Mu_y = FindCentroid(Y);
           
                ///P matrisinin X ve Y koordinatlarini iceren satirlari farkli matrislere aliniyor
                dummy_p1.SetRow(0, P_points.Row(0));
                dummy_p2.SetRow(0, P_points.Row(1));
                if (_3D)
                {
                    dummy_p3.SetRow(0, P_points.Row(2));
                }
                    ///P_points is moved to origin by subtructing centroid from every element.
                    /// P deki her bir noktadan p nin agirlik merkezinin koordinatlari cikartiliyor(ZERO MEAN) yeni bir matris icerisine kaydediliyor.
                    Matrix<double> P_prime = (dummy_p1 - Mu_p[0, 0]).Stack((dummy_p2 - Mu_p[1, 0]).Stack(dummy_p3 - Mu_p[2, 0]));
                ///Calculate Centroid for both point clouds
                ///Y matrisinin X ve Y koordinatlarini iceren satirlari farkli matrislere aliniyor
                dummy_y1.SetRow(0, Y.Row(0));
                dummy_y2.SetRow(0, Y.Row(1));
                if (_3D)
                {
                    dummy_y3.SetRow(0, Y.Row(2));
                }
                    ///M_points is moved to origin by subtructing centroid from every element.
                    /// P deki her bir noktadan p nin agirlik merkezinin koordinatlari cikartiliyor(ZERO MEAN) yeni bir matris icerisine kaydediliyor.
                    Matrix<double> Y_prime = (dummy_y1 - Mu_y[0, 0]).Stack((dummy_y2 - Mu_y[1, 0]).Stack(dummy_y3 - Mu_y[2, 0]));
                
                /// -X -Y -Z koordinat matrisleri aliniyor.                
                Px.SetRow(0, P_prime.Row(0));
                Py.SetRow(0, P_prime.Row(1));
                if (_3D)
                {
                    Pz.SetRow(0, P_prime.Row(2));
                    Yz.SetRow(0, Y_prime.Row(2));
                }
                Yx.SetRow(0, Y_prime.Row(0));
                Yy.SetRow(0, Y_prime.Row(1));
                

                var Sxx = Px * Yx.Transpose();
                var Sxy = Px * Yy.Transpose();
                var Sxz = Px * Yz.Transpose();

                var Syx = Py * Yx.Transpose();
                var Syy = Py * Yy.Transpose();
                var Syz = Py * Yz.Transpose();

                var Szx = Pz * Yx.Transpose();
                var Szy = Pz * Yy.Transpose();
                var Szz = Pz * Yz.Transpose();
                Matrix<double> Nmatrix = m.DenseOfArray(new[,]{{ Sxx[0, 0] + Syy[0, 0] + Szz[0, 0],  Syz[0, 0] - Szy[0, 0],       -Sxz[0, 0] + Szx[0, 0],        Sxy[0, 0] - Syx[0, 0]},
                                                {-Szy[0, 0] + Syz[0, 0],        Sxx[0, 0] - Syy[0, 0] - Szz[0, 0],  Sxy[0, 0] + Syx[0, 0],        Sxz[0, 0] + Szx[0, 0]},
                                                {Szx[0, 0] - Sxz[0, 0],         Syx[0, 0] + Sxy[0, 0],       -Sxx[0, 0] + Syy[0, 0] - Szz[0, 0],  Syz[0, 0] + Szy[0, 0]},
                                                {-Syx[0, 0] + Sxy[0, 0],        Szx[0, 0] + Sxz[0, 0],        Szy[0, 0] + Syz[0, 0],       -Sxx[0, 0] + Szz[0, 0] - Syy[0, 0]} });
                
                var evd = Nmatrix.Evd();
                Matrix<double> eigenvectors = evd.EigenVectors;
                var q = eigenvectors.Column(3);
                var q0 = q[0]; var q1 = q[1]; var q2 = q[2]; var q3 = q[3];

                ///Quernion matrix is calculated
                ///Quernion matrislerinin bulunmasi
                var Qbar = m.DenseOfArray(new[,] { { q0, -q1, -q2, -q3 },
                                               { q1, q0, q3, -q2 },
                                               { q2, -q3, q0, q1 },
                                               { q3, q2, -q1, q0 }});

                var Q = m.DenseOfArray(new[,] {    { q0, -q1, -q2, -q3 },
                                               { q1, q0, -q3, q2 },
                                               { q2, q3, q0, -q1 },
                                               { q3, -q2, q1, q0 }});
                ///Calculating Rotation matrix
                ///Rotasyon matrisi hesabi
                R = (Qbar.Transpose()).Multiply(Q);
                R = (R.RemoveColumn(0)).RemoveRow(0);

                ///Translation hesabi
                t = Mu_y - s * R * Mu_p;

                var ref_vector2 = R.Multiply(ref_vector.Stack(Third_raw1));
                var Px3 = m.Dense(1, 1);
                var Py3 = m.Dense(1, 1);
                Px3.SetRow(0, ref_vector2.Row(0));
                Py3.SetRow(0, ref_vector2.Row(1));
                //Pz2.SetRow(0, P_points2.Row(2));
                Px3 = Px3 + t[0, 0];
                Py3 = Py3 + t[1, 0];
                //Pz2=Pz2 + t[2, 0];
                ref_vector.SetRow(0, Px3.Row(0));
                ref_vector.SetRow(1, Py3.Row(0));
                ///SCALE Factor hesabi
                //
                ///          
                ///Transformation is applied to P_points
                ///Hesaplanan değerler P_points matrisine uygulanıyor
                if (!_3D)
                {
                    P_points2 = R.Multiply(P_points.Stack(Third_raw));
                    Px2.SetRow(0, P_points2.Row(0));
                    Py2.SetRow(0, P_points2.Row(1));
                    //Pz2.SetRow(0, P_points2.Row(2));
                    Px2 = Px2 + t[0, 0];
                    Py2 = Py2 + t[1, 0];
                    //Pz2=Pz2 + t[2, 0];
                    P_points.SetRow(0, Px2.Row(0));
                    P_points.SetRow(1, Py2.Row(0));
                    //P_points.SetRow(2, Pz2.Row(0));
                    Matrix<double> pp = P_points.Stack(Third_raw);   ///For 3-D clouds, this line should be commentted out and replace the pp with P_points in following lines
                                                                     ///error calculation
                                                                     ///hata hesabi
                    for (int i = 0; i < Np; i++)
                    {
                        d = Y.Column(i).Subtract(pp.Column(i));
                        err += d[0] * d[0] + d[1] * d[1] + d[2] * d[2];////
                    }
                }
                else
                {
                    P_points2 = R.Multiply(P_points);
                    Px2.SetRow(0, P_points2.Row(0));
                    Py2.SetRow(0, P_points2.Row(1));
                    Pz2.SetRow(0, P_points2.Row(2));
                    Px2 = Px2 + t[0, 0];
                    Py2 = Py2 + t[1, 0];
                    Pz2=Pz2 + t[2, 0];
                    P_points.SetRow(0, Px2.Row(0));
                    P_points.SetRow(1, Py2.Row(0));
                    P_points.SetRow(2, Pz2.Row(0));
                    for (int i = 0; i < Np; i++)
                    {
                        d = Y.Column(i).Subtract(P_points.Column(i));
                        err += d[0] * d[0] + d[1] * d[1] + d[2] * d[2];////
                    }
                }
                err = err / Np;
                ///checking the conditions of convergence
                ///koşullar kontrol ediliyor.
                if (err < threshold || Math.Abs(previous_error - err) < delta_error_thresh)
                {
                    Debug.WriteLine("iteration= " + itr);
                    Debug.WriteLine("error= " + err);
                    break;                    
                }                
                previous_error = err;
                err = 0;                
            }
            return P_points;
        }

        /// <summary>
        /// This Function Calculates Final P_points under the conditions of either threshold or maximum iterations that given as input.
        /// Returns matched P_points.
        /// </summary>
        /// <param name="M_points"></param>
        /// <param name="P_points"></param>
        /// <param name="threshold"></param>
        /// <param name="max_iterations"></param>
        /// <param name="_3D"></param>
        /// <returns></returns>
        public static Matrix<double> ICP_run(Matrix<double> M_points, Matrix<double> P_points, double threshold, int max_iterations, bool _3D)
        {
            #region "Definitions"
            var m = Matrix<double>.Build;
            int Np = P_points.ColumnCount;
            Matrix<double> dummy_p1 = m.Dense(1, Np);
            Matrix<double> dummy_p2 = m.Dense(1, Np);
            Matrix<double> dummy_p3 = m.Dense(1, Np, 0);
            Matrix<double> dummy_y1 = m.Dense(1, Np);
            Matrix<double> dummy_y2 = m.Dense(1, Np);
            Matrix<double> dummy_y3 = m.Dense(1, Np, 0);
            Matrix<double> Px = m.Dense(1, Np);
            Matrix<double> Py = m.Dense(1, Np);
            Matrix<double> Pz = m.Dense(1, Np, 0);
            Matrix<double> Yx = m.Dense(1, Np);
            Matrix<double> Yy = m.Dense(1, Np);
            Matrix<double> Yz = m.Dense(1, Np, 0);
            Matrix<double> dummy_Row = m.Dense(1, Np, 0);
            Matrix<double> Third_raw = m.Dense(1, Np, 0); ///dummy row to be used in calculations
            Matrix<double> Px2 = m.Dense(1, Np);
            Matrix<double> Py2 = m.Dense(1, Np);
            Matrix<double> Pz2 = m.Dense(1, Np);
            Matrix<double> P_points2;
            double s = 1;
            double delta_error_thresh = 0.00001;
            double previous_error = 0;

            Matrix<double> Third_raw1 = m.Dense(1, 1, 0); ///dummy row to be used in calculations

            Matrix<double> R;
            Matrix<double> t;
            double err = 0;
            Matrix<double> Y;
            Vector<double> d;

            Matrix<double> TransformationMat = m.DenseIdentity(3, 3);
            
            #endregion
            for (int itr = 1; itr <= max_iterations; itr++)
            {
                Y = KD_tree(M_points, P_points);
                ///Calculate Centroid for both point clouds
                ///P ve Y matrislerinin agirlik merkezi hesaplaniyor
                Matrix<double> Mu_p = FindCentroid(P_points);
                Matrix<double> Mu_y = FindCentroid(Y);

                ///P matrisinin X ve Y koordinatlarini iceren satirlari farkli matrislere aliniyor
                dummy_p1.SetRow(0, P_points.Row(0));
                dummy_p2.SetRow(0, P_points.Row(1));
                if (_3D)
                {
                    dummy_p3.SetRow(0, P_points.Row(2));
                }
                ///P_points is moved to origin by subtructing centroid from every element.
                /// P deki her bir noktadan p nin agirlik merkezinin koordinatlari cikartiliyor(ZERO MEAN) yeni bir matris icerisine kaydediliyor.
                Matrix<double> P_prime = (dummy_p1 - Mu_p[0, 0]).Stack((dummy_p2 - Mu_p[1, 0]).Stack(dummy_p3 - Mu_p[2, 0]));
                ///Calculate Centroid for both point clouds
                ///Y matrisinin X ve Y koordinatlarini iceren satirlari farkli matrislere aliniyor
                dummy_y1.SetRow(0, Y.Row(0));
                dummy_y2.SetRow(0, Y.Row(1));
                if (_3D)
                {
                    dummy_y3.SetRow(0, Y.Row(2));
                }
                ///M_points is moved to origin by subtructing centroid from every element.
                /// P deki her bir noktadan p nin agirlik merkezinin koordinatlari cikartiliyor(ZERO MEAN) yeni bir matris icerisine kaydediliyor.
                Matrix<double> Y_prime = (dummy_y1 - Mu_y[0, 0]).Stack((dummy_y2 - Mu_y[1, 0]).Stack(dummy_y3 - Mu_y[2, 0]));

                /// -X -Y -Z koordinat matrisleri aliniyor.                
                Px.SetRow(0, P_prime.Row(0));
                Py.SetRow(0, P_prime.Row(1));
                if (_3D)
                {
                    Pz.SetRow(0, P_prime.Row(2));
                    Yz.SetRow(0, Y_prime.Row(2));
                }
                Yx.SetRow(0, Y_prime.Row(0));
                Yy.SetRow(0, Y_prime.Row(1));


                var Sxx = Px * Yx.Transpose();
                var Sxy = Px * Yy.Transpose();
                var Sxz = Px * Yz.Transpose();

                var Syx = Py * Yx.Transpose();
                var Syy = Py * Yy.Transpose();
                var Syz = Py * Yz.Transpose();

                var Szx = Pz * Yx.Transpose();
                var Szy = Pz * Yy.Transpose();
                var Szz = Pz * Yz.Transpose();
                Matrix<double> Nmatrix = m.DenseOfArray(new[,]{{ Sxx[0, 0] + Syy[0, 0] + Szz[0, 0],  Syz[0, 0] - Szy[0, 0],       -Sxz[0, 0] + Szx[0, 0],        Sxy[0, 0] - Syx[0, 0]},
                                                {-Szy[0, 0] + Syz[0, 0],        Sxx[0, 0] - Syy[0, 0] - Szz[0, 0],  Sxy[0, 0] + Syx[0, 0],        Sxz[0, 0] + Szx[0, 0]},
                                                {Szx[0, 0] - Sxz[0, 0],         Syx[0, 0] + Sxy[0, 0],       -Sxx[0, 0] + Syy[0, 0] - Szz[0, 0],  Syz[0, 0] + Szy[0, 0]},
                                                {-Syx[0, 0] + Sxy[0, 0],        Szx[0, 0] + Sxz[0, 0],        Szy[0, 0] + Syz[0, 0],       -Sxx[0, 0] + Szz[0, 0] - Syy[0, 0]} });

                var evd = Nmatrix.Evd();
                Matrix<double> eigenvectors = evd.EigenVectors;
                var q = eigenvectors.Column(3);
                var q0 = q[0]; var q1 = q[1]; var q2 = q[2]; var q3 = q[3];

                ///Quernion matrix is calculated
                ///Quernion matrislerinin bulunmasi
                var Qbar = m.DenseOfArray(new[,] { { q0, -q1, -q2, -q3 },
                                               { q1, q0, q3, -q2 },
                                               { q2, -q3, q0, q1 },
                                               { q3, q2, -q1, q0 }});

                var Q = m.DenseOfArray(new[,] {    { q0, -q1, -q2, -q3 },
                                               { q1, q0, -q3, q2 },
                                               { q2, q3, q0, -q1 },
                                               { q3, -q2, q1, q0 }});
                ///Calculating Rotation matrix
                ///Rotasyon matrisi hesabi
                R = (Qbar.Transpose()).Multiply(Q);
                R = (R.RemoveColumn(0)).RemoveRow(0);

                ///Translation hesabi
                t = Mu_y - s * R * Mu_p;

                TransformationMat= CreateTransformationMat(R, t).Multiply(TransformationMat);
       
                ///SCALE Factor hesabi
                //
                ///          
                ///Transformation is applied to P_points
                ///Hesaplanan değerler P_points matrisine uygulanıyor
                if (!_3D)
                {
                    P_points2 = R.Multiply(P_points.Stack(Third_raw));
                    Px2.SetRow(0, P_points2.Row(0));
                    Py2.SetRow(0, P_points2.Row(1));
                    //Pz2.SetRow(0, P_points2.Row(2));
                    Px2 = Px2 + t[0, 0];
                    Py2 = Py2 + t[1, 0];
                    //Pz2=Pz2 + t[2, 0];
                    P_points.SetRow(0, Px2.Row(0));
                    P_points.SetRow(1, Py2.Row(0));
                    //P_points.SetRow(2, Pz2.Row(0));
                    Matrix<double> pp = P_points.Stack(Third_raw);   ///For 3-D clouds, this line should be commentted out and replace the pp with P_points in following lines
                                                                     ///error calculation
                                                                     ///hata hesabi
                    for (int i = 0; i < Np; i++)
                    {
                        d = Y.Column(i).Subtract(pp.Column(i));
                        err += d[0] * d[0] + d[1] * d[1] + d[2] * d[2];////
                    }
                }
                else
                {
                    P_points2 = R.Multiply(P_points);
                    Px2.SetRow(0, P_points2.Row(0));
                    Py2.SetRow(0, P_points2.Row(1));
                    Pz2.SetRow(0, P_points2.Row(2));
                    Px2 = Px2 + t[0, 0];
                    Py2 = Py2 + t[1, 0];
                    Pz2 = Pz2 + t[2, 0];
                    P_points.SetRow(0, Px2.Row(0));
                    P_points.SetRow(1, Py2.Row(0));
                    P_points.SetRow(2, Pz2.Row(0));
                    for (int i = 0; i < Np; i++)
                    {
                        d = Y.Column(i).Subtract(P_points.Column(i));
                        err += d[0] * d[0] + d[1] * d[1] + d[2] * d[2];////
                    }
                }
                err = err / Np;
                ///checking the conditions of convergence
                ///koşullar kontrol ediliyor.
                if (err < threshold || Math.Abs(previous_error - err) < delta_error_thresh)
                {
                    Debug.WriteLine("iteration= " + itr);
                    Debug.WriteLine("error= " + err);
                    break;
                }
                previous_error = err;
                err = 0;
            }
            return TransformationMat;
        }
        /// <summary>
        /// Calculates Nearest Neighbour for each points and returns distance and coordinate
        /// </summary>
        /// <param name="M_points"></param>
        /// <param name="P_points"></param>
        /// <returns></returns>
        public static Matrix<double> KD_tree(Matrix<double> M_points, Matrix<double> P_points)
        {
            int Np = P_points.ColumnCount;

            /// this function is used for calculating the Euclidean norm
            Func<double[], double[], double> L2Norm = (x, y) =>
            {
                double dist = 0;
                for (int i = 0; i < x.Length; i++)
                {
                    dist += (x[i] - y[i]) * (x[i] - y[i]);///squre root is not used to speed up
                }
                return dist;
            };
            ///create tree of M_points
            var treeData2 = M_points.ToColumnArrays();
            var treeNodes = treeData2.Select(p => p.ToString()).ToArray();
            var m = Matrix<double>.Build;
            Matrix<double> Y = m.Dense(3, Np,0);
            Tuple<double[], string>[] test;
            var tree = new KDTree<double, string>(2, treeData2, treeNodes, L2Norm);
            var scan_data = P_points.ToColumnArrays();
            ///Calculate nearest neighbour for every element of cloud
            for (int i = 0; i < scan_data.Length; i++)
            {
                test = tree.NearestNeighbors(scan_data[i], 1);
                Y[0, i] = test[0].Item1[0];
                Y[1, i] = test[0].Item1[1];
            }
            return Y;///return result matrix
        }
        /// <summary>
        /// Finds the center point of given data cloud and returns a matrix of coordinates.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Matrix<double> FindCentroid(Matrix<double> points)
        {
            var m = Matrix<double>.Build;
            int column_count = points.ColumnCount;
            Matrix<double> centroid = m.Dense(3, 1, 0);
            double TotalX = 0;
            double TotalY = 0;
            //double TotalZ = 0;
            double AvrX = 0;
            double AvrY = 0;
            double AvrZ = 0;

            for (int i = 0; i < column_count; i++)
            {
                TotalX += points[0, i];
                TotalY += points[1, i];
                //TotalZ += points[2, i];
            }
            AvrX = TotalX / column_count;
            AvrY = TotalY / column_count;
            //AvrZ = TotalZ / column_count;

            centroid[0, 0] = AvrX;
            centroid[1, 0] = AvrY;
            centroid[2, 0] = AvrZ;
            return centroid;
        }
        public static Matrix<double> AddVectorValsToMatrix(Matrix<double> matrix, Matrix<double> vector)
        {
            var m = Matrix<double>.Build;
            var retMatrix = matrix.Clone();
            int columnCount = matrix.ColumnCount;
            var Px = m.Dense(1, columnCount);
            var Py = m.Dense(1, columnCount);
            Px.SetRow(0, matrix.Row(0));
            Py.SetRow(0, matrix.Row(1));
            Px = Px + vector[0, 0];
            Py = Py + vector[1, 0];
            retMatrix.SetRow(0, Px.Row(0));
            retMatrix.SetRow(1, Py.Row(0));
            return retMatrix;
        }

        /// <summary>
        /// Creates 2D transformation matrix out of given Rotation matrix and translation vector
        /// </summary>
        /// <param name="R">Rotation matrix (3x3)</param>
        /// <param name="t">translation vector(3x1)</param>
        /// <returns></returns>
        public static Matrix<double>CreateTransformationMat(Matrix<double> R,Matrix<double> t)
        {
            Matrix<double> transformationMatrix = R.Clone();
            transformationMatrix.SetColumn(2, t.Column(0));
            transformationMatrix.SetRow(2, new double[] { 0, 0, 1 });
            return transformationMatrix;
        }
    }
}
