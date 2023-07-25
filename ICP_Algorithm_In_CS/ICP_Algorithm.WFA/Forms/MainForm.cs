/// ===============================
/// AUTHOR       :Yahya Sevikoglu - yahyasevikoglu@gmail.com
/// CREATE DATE  :29.11.2019
/// PURPOSE      : This class is used for calculation of convergens between two point clouds. These point clouds should be same dimensional but can have different number of sample points.

using ICP_Algorithm.WFA.Forms;
using ICP_Algorithm.WFA.Languages;
using ICP_Algorithm.WFA.Methods;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ICP_Algorithm.WFA
{
    public partial class MainForm : Form
    {
        double previous_error;
        double delta_error_thresh = 0.0001;
        public MainForm()
        {
            InitializeComponent();
            GetLang();
        }

        private void GetLang()
        {
            this.Text = Localization.MainFormTitle;

            #region TopMenu
            fileToolStripMenuItem.Text = Localization.File;
            exitToolStripMenuItem.Text = Localization.Exit;

            toolsToolStripMenuItem.Text = Localization.Tools;
            optionsToolStripMenuItem.Text = Localization.Options;

            helpToolStripMenuItem.Text = Localization.Help;
            reportAnIssueToolStripMenuItem.Text = Localization.ReportAnIssue;
            githubWebPageToolStripMenuItem.Text = Localization.githubWebPage;
            aTutorialOnRigidRegistrationToolStripMenuItem.Text = Localization.aTutorialOnRigidRegistration;
            aboutToolStripMenuItem.Text = Localization.About;
            #endregion

            grpBoxInitialTopLeft.Text = Localization.grpBoxInitialTopLeft;
            grpBoxTransformationTopLeft.Text = Localization.grpBoxTransformationTopLeft;

            btnNoisySample.Text = Localization.NoisySample;
            btn_normal_sample.Text = Localization.NormalSample;
            btnShowFinal.Text = Localization.ShowFinal;

            lbAngle.Text = Localization.Angle;
            lbAngle2.Text = Localization.Angle;
            lbX.Text = Localization.X;
            lbX2.Text = Localization.X;
            lbY.Text = Localization.Y;
            lbY2.Text = Localization.Y;
        }

        private void btnShowFinal_Click(object sender, EventArgs e)
        {
            ///definitions
            var m = Matrix<double>.Build;
            Matrix<double> R = m.Dense(3, 3);
            Matrix<double> t = m.Dense(3, 1);
            double offset_x = 0, offset_y = 120; ///translation respect to origin
            double alfa = 0; ///rotation respect to origin
            offset_x = Convert.ToDouble(textBox1.Text);
            offset_y = Convert.ToDouble(textBox2.Text);
            alfa = Convert.ToDouble(textBox3.Text);

            V_Shape_ICP v_Shape_ICP = new V_Shape_ICP();

            double init_offsetX = Convert.ToDouble(Txt_InitPosX.Text);
            double init_offsetY = Convert.ToDouble(Txt_InitPosY.Text);
            v_Shape_ICP.AngleOfV_Obj = Convert.ToDouble(Txt_InitAngle.Text);
            v_Shape_ICP.PositionOfV_Obj[0] = init_offsetX;
            v_Shape_ICP.PositionOfV_Obj[1] = init_offsetY;

            //Matrix<double> M_points = CreateCircle(grid, Np, 10, 350);
            Matrix<double> M_points = v_Shape_ICP.CreateRefMatrix();

            ///Create C shaped Data cloud
           // Matrix<double> M_points = CreateCircle(grid, Np, 10, 350);
            ///transform this data cloud for specified parameters as Model cloud and copy them to another cloud as sample
            Matrix<double> P_points = ICP.Transform(M_points, alfa, new double[] { offset_x, offset_y }, new double[] { init_offsetX, init_offsetY });
            ///Add random noise to Sample Data cloud
            ///Create and draw data images
            Bitmap flag = new Bitmap(1000, 1000);
            Graphics g = Graphics.FromImage(flag);
            g.Clear(Color.Black);
            pictureBox1.Image = flag;
            DrawPoint2(P_points, Brushes.Red, pictureBox1, g);
            DrawPoint2(M_points, Brushes.Green, pictureBox1, g);
            ///delay 1500 ms for user to see data clouds
            System.Threading.Thread.Sleep(2000);
            Stopwatch watch = new Stopwatch();
            watch.Restart();
            //Matrix<double> ref_Vector=m.Dense(2,1,1);
            //Matrix<double> ref_Vector_ref = m.Dense(2, 1, 1);
            //ref_Vector[0, 0] = 10;
            //ref_Vector[1, 0] = 350 ;
            //ref_Vector = addMotion(ref_Vector, offset_x, offset_y, alfa);
            //ref_Vector_ref = ref_Vector.Clone();
            //V_Shape_ICP v_Shape_ICP = new V_Shape_ICP();
            Matrix<double> offsetMat = m.Dense(2, 1);
            offsetMat[0, 0] = init_offsetX;
            offsetMat[1, 0] = init_offsetY;
            var cornerPoints2 = v_Shape_ICP.FindVShapePoints(P_points);
            //var cornerPoints = ICP.AddVectorValsToMatrix(cornerPoints2, offsetMat);

            //leftCorner = (double[])cornerPoints[0].Clone();
            //midPoint = (double[])cornerPoints[1].Clone();
            //rightCorner = (double[])cornerPoints[2].Clone();


            ///Apply ICP  to Point clouds
            // P_points = ICP.ICP_run(M_points, P_points, threshold, max_itr,false,ref ref_Vector);
            watch.Stop();
            Debug.WriteLine(Localization.ICPtime + watch.ElapsedMilliseconds + " " + Localization.ms);
            ///Draw Results
            g.Clear(Color.Black);
            pictureBox1.Image = flag;
            DrawPoint2(P_points, Brushes.Red, pictureBox1, g);
            DrawPoint2(cornerPoints2, Brushes.White, pictureBox1, g);
            DrawPoint2(M_points, new SolidBrush(Color.FromArgb(30, Color.Green)), pictureBox1, g);
            //DrawPoint2(ref_Vector, Brushes.Blue, pictureBox1, g);
            //DrawPoint2(ref_Vector_ref, Brushes.White, pictureBox1, g);
        }

        private void btnNoisySample_Click(object sender, EventArgs e)
        {
            var m = Matrix<double>.Build;
            int max_itr = 5000;
            double grid = 0.1;
            double err = 0;

            int Np = 500;
            double threshold = 0.00001;
            Matrix<double> R = m.Dense(3, 3);
            Matrix<double> t = m.Dense(3, 1);
            double offset_x = 0, offset_y = 120;
            double alfa = 0;
            offset_x = Convert.ToDouble(textBox1.Text);
            offset_y = Convert.ToDouble(textBox2.Text);
            alfa = Convert.ToDouble(textBox3.Text);
            Matrix<double> M_points = CreateCircle(grid, Np, 10, 350);
            Matrix<double> P_points = addMotion(M_points, offset_x, offset_y, alfa);

            Random r = new Random();
            for (int i = 0; i < Np; i++)
            {
                P_points[0, i] += (r.NextDouble() - 0.5) * 5;
                P_points[1, i] += (r.NextDouble() - 0.5) * 5;
            }
            Bitmap flag = new Bitmap(1000, 1000);
            Graphics g = Graphics.FromImage(flag);
            g.Clear(Color.Black);
            pictureBox1.Image = flag;
            DrawPoint2(P_points, Brushes.Red, pictureBox1, g);
            DrawPoint2(M_points, Brushes.Green, pictureBox1, g);
            System.Threading.Thread.Sleep(1500);
            Tuple<Matrix<double>, Matrix<double>, double> ret;
            Stopwatch watch = new Stopwatch();
            Stopwatch watch2 = new Stopwatch();
            double time = 0;
            for (int itr = 1; itr <= max_itr; itr++)
            {
                watch.Restart();

                ret = ICP.ICP_run(M_points, P_points, false);
                R = ret.Item1;
                t = ret.Item2;
                err = ret.Item3;

                Matrix<double> Third_raw = m.Dense(1, Np, 0);
                Matrix<double> Px = m.Dense(1, Np);
                Matrix<double> Py = m.Dense(1, Np);
                Matrix<double> P_points2;
                P_points2 = R.Multiply(P_points.Stack(Third_raw));
                Px.SetRow(0, P_points2.Row(0));
                Py.SetRow(0, P_points2.Row(1));
                Px = Px + t[0, 0];
                Py = Py + t[1, 0];
                P_points.SetRow(0, Px.Row(0));
                P_points.SetRow(1, Py.Row(0));
                watch.Stop();
                time += watch.ElapsedMilliseconds;
                g.Clear(Color.Black);
                pictureBox1.Image = flag;
                DrawPoint2(P_points, Brushes.Red, pictureBox1, g);
                DrawPoint2(M_points, Brushes.Green, pictureBox1, g);
                if (err < threshold || Math.Abs(previous_error - err) < delta_error_thresh)
                {
                    Debug.WriteLine(Localization.error + err);
                    Debug.WriteLine(Localization.iteration + itr);
                    break;
                }
                previous_error = err;
            }
            Debug.WriteLine(Localization.Time + time + Localization.ms);
        }
        private void btn_normal_sample_Click(object sender, EventArgs e)
        {
            var m = Matrix<double>.Build;
            int max_itr = 5000;
            double grid = 0.1;
            double err = 0;

            double threshold = 0.00001;
            Matrix<double> R = m.Dense(3, 3);
            Matrix<double> t = m.Dense(3, 1);
            double offset_x = 0, offset_y = 120;
            double alfa = 0;
            offset_x = Convert.ToDouble(textBox1.Text);
            offset_y = Convert.ToDouble(textBox2.Text);
            alfa = Convert.ToDouble(textBox3.Text);

            V_Shape_ICP v_Shape_ICP = new V_Shape_ICP();

            v_Shape_ICP.AngleOfV_Obj = 0;
            v_Shape_ICP.PositionOfV_Obj[0] = 100;
            v_Shape_ICP.PositionOfV_Obj[1] = 350;

            //Matrix<double> M_points = CreateCircle(grid, Np, 10, 350);
            Matrix<double> M_points = v_Shape_ICP.CreateRefMatrix();
            int Np = M_points.ColumnCount;

            Matrix<double> P_points = addMotion(M_points, offset_x, offset_y, alfa);

            Bitmap flag = new Bitmap(1000, 1000);
            Graphics g = Graphics.FromImage(flag);
            g.Clear(Color.Black);
            pictureBox1.Image = flag;
            DrawPoint2(P_points, Brushes.Red, pictureBox1, g);
            DrawPoint2(M_points, Brushes.Green, pictureBox1, g);
            System.Threading.Thread.Sleep(1500);
            Tuple<Matrix<double>, Matrix<double>, double> ret;
            Stopwatch watch = new Stopwatch();
            Stopwatch watch2 = new Stopwatch();
            double time = 0;
            for (int itr = 1; itr <= max_itr; itr++)
            {
                watch.Restart();
                ret = ICP.ICP_run(M_points, P_points, false);
                R = ret.Item1;
                t = ret.Item2;
                err = ret.Item3;

                Matrix<double> Third_raw = m.Dense(1, Np, 0);
                Matrix<double> Px = m.Dense(1, Np);
                Matrix<double> Py = m.Dense(1, Np);
                Matrix<double> P_points2;
                P_points2 = R.Multiply(P_points.Stack(Third_raw));
                Px.SetRow(0, P_points2.Row(0));
                Py.SetRow(0, P_points2.Row(1));
                Px = Px + t[0, 0];
                Py = Py + t[1, 0];
                P_points.SetRow(0, Px.Row(0));
                P_points.SetRow(1, Py.Row(0));
                watch.Stop();
                time += watch.ElapsedMilliseconds;
                g.Clear(Color.Black);
                pictureBox1.Image = flag;
                DrawPoint2(P_points, Brushes.Red, pictureBox1, g);
                DrawPoint2(M_points, Brushes.Green, pictureBox1, g);
                if (err < threshold || Math.Abs(previous_error - err) < delta_error_thresh)
                {
                    Debug.WriteLine(Localization.error + err);
                    Debug.WriteLine(Localization.iteration + itr);
                    break;
                }
                previous_error = err;
            }
            Debug.WriteLine(Localization.Time + time + Localization.ms);
        }

        public Matrix<double> addMotion(Matrix<double> cpoints, double dx, double dy, double dtheta)
        {
            var m = Matrix<double>.Build;
            Matrix<double> Rt = m.Dense(2, 2);
            Matrix<double> Tt = m.Dense(2, 1);
            Vector<double> dummycpointsV;
            Matrix<double> dummycpointsM;
            int nPoint = cpoints.ColumnCount;
            double dthetainRad = Math.PI * dtheta / 180;
            Rt[0, 0] = Math.Cos(dthetainRad);
            Rt[1, 0] = -Math.Sin(dthetainRad);
            Rt[0, 1] = -Rt[1, 0];
            Rt[1, 1] = Rt[0, 0];

            Tt[0, 0] = dx;
            Tt[1, 0] = dy;

            cpoints = Rt.Multiply(cpoints);
            dummycpointsV = cpoints.Column(0) + Tt.Column(0);
            dummycpointsM = dummycpointsV.ToColumnMatrix();

            for (int i = 1; i < nPoint; i++)
            {
                dummycpointsV = cpoints.Column(i) + Tt.Column(0);
                dummycpointsM = dummycpointsM.Append(dummycpointsV.ToColumnMatrix());
            }
            cpoints = dummycpointsM;

            return cpoints;
        }

        public Matrix<double> CreateCircle(double grid, int len, int offset_x, int offset_y)
        {
            var m = Matrix<double>.Build;
            Matrix<double> points = m.Dense(2, len, 0);
            int j = 0;
            double r = len * grid / 2;
            for (int i = 0; i < len; i++)
            {
                points[0, i] = j * grid + offset_x;
                if (i == len / 2)
                {
                    j = 0;
                }

                j++;
            }
            for (int i = 0; i < len; i++)
            {
                if (i <= len / 2)
                {
                    points[1, i] = Math.Sqrt((r - points[0, i] + offset_x) * (r + points[0, i] - offset_x)) + offset_y;
                }
                else
                {
                    points[1, i] = -Math.Sqrt((r - points[0, i] + offset_x) * (r + points[0, i] - offset_x)) + offset_y;
                }
                //points[1, i] = Math.Sqrt((r - points[0, i] + offset_x) * (r + points[0, i] - offset_x)) + offset_y;
            }
            return points;
        }

        public void DrawPoint2(Matrix<double> point, Brush b, PictureBox p, Graphics g)
        {
            try
            {
                int nPoint = point.ColumnCount;
                int[] x = new int[nPoint];
                int[] y = new int[nPoint];
                for (int j = 0; j < nPoint; j++)
                {
                    x[j] = (int)Math.Round(point.At(0, j));
                    y[j] = (int)Math.Round(point.At(1, j));
                }

                for (int i = 0; i < nPoint; i++)
                {
                    g.FillRectangle(b, x[i], y[i], 3, 3);
                }
                p.Refresh();
            }
            catch
            {

            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowOptionsForm();
        }

        private void ShowOptionsForm()
        {
            OptionsFrm optionsFrm = new OptionsFrm();
            optionsFrm.ShowDialog();
        }

        private void reportAnIssueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Websites.OpenGithubIssues();
        }

        private void githubWebPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Websites.OpenGithubProject();
        }

        private void aTutorialOnRigidRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Websites.OpenATutorialOnRigidRegistration();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }
    }
}