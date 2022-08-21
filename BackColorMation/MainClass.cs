using System;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace BackColorMation
{
    public class ColorMation
    {
        public ColorMation(Control[] Targets, bool DifferentColorsAnyway = false)
        {
            foreach (Control Target in Targets)
            {
                new Thread(delegate () { DoColorChange(Target); }) { IsBackground = true }.Start();
                if (DifferentColorsAnyway) Thread.Sleep(1);
            }
        }

        internal void DoColorChange(Control Target)
        {
            Random RND = new Random();
            while (true)
            {
                int NewR = RND.Next(0, 255);
                int NewG = RND.Next(0, 255);
                int NewB = RND.Next(0, 255);
                Parallel.Invoke(
                    () => Change_R(NewR, Target),
                    () => Change_G(NewG, Target),
                    () => Change_B(NewB, Target)
                    );
                Thread.Sleep(1);
            }

        }

        private void Change_R(int NewR, Control Target)
        {
            int CurR = Target.BackColor.R;
            if (NewR > CurR)
                for (; CurR != NewR; CurR++)
                {
                    Target.BackColor = Color.FromArgb(CurR, Target.BackColor.G, Target.BackColor.B);
                    Thread.Sleep(1);
                }
            else
                for (; CurR != NewR; CurR--)
                {
                    Target.BackColor = Color.FromArgb(CurR, Target.BackColor.G, Target.BackColor.B);
                    Thread.Sleep(1);
                }
        }

        private void Change_G(int NewG, Control Target)
        {
            int CurG = Target.BackColor.G;
            if (NewG > CurG)
                for (; CurG != NewG; CurG++)
                {
                    Target.BackColor = Color.FromArgb(Target.BackColor.R, CurG, Target.BackColor.B);
                    Thread.Sleep(1);
                }
            else
                for (; CurG != NewG; CurG--)
                {
                    Target.BackColor = Color.FromArgb(Target.BackColor.R, CurG, Target.BackColor.B);
                    Thread.Sleep(1);
                }
        }

        private void Change_B(int NewB, Control Target)
        {
            int CurB = Target.BackColor.B;
            if (NewB > CurB)
                for (; CurB != NewB; CurB++)
                {
                    Target.BackColor = Color.FromArgb(Target.BackColor.R, Target.BackColor.G, CurB);
                    Thread.Sleep(1);
                }
            else
                for (; CurB != NewB; CurB--)
                {
                    Target.BackColor = Color.FromArgb(Target.BackColor.R, Target.BackColor.G, CurB);
                    Thread.Sleep(1);
                }
        }

    }
}
