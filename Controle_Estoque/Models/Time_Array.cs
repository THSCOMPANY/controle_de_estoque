using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Controle_Estoque
{
    class Time_Array
    {
        static void main()
        {
            int[] nums = new int[100000];
            BuildArray(nums);

            TimeSpan startTime;
            TimeSpan duration;

            startTime = Process.GetCurrentProcess().Threads[0].UserProcessorTime;
            Display(nums);

            duration = Process.GetCurrentProcess().Threads[0].UserProcessorTime.Subtract(startTime);
            System.Windows.Forms.MessageBox.Show("Time" + duration.TotalSeconds);
        }
        
        static void BuildArray(int[] array)
        {
            for (int i = 0; i <= 99999; i++)
                array[i] = i;
        }

        static void Display(int[] array)
        {
            for (int i = 0; i <= array.GetUpperBound(0); i++)
                System.Windows.Forms.MessageBox.Show(array[i] + " ");
        }
    }

    
}
