using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//TODO Add keyboard and pad controller on SharpDx
namespace Controller
{
    public class Test
    {
        public Test()
        {
            var directInput = new DirectInput();

            var keyboard = new Keyboard(directInput);
            keyboard.Properties.BufferSize = 128;
            keyboard.Acquire();

            while (true) 
            {
                keyboard.Poll();
                var datas = keyboard.GetBufferedData();
                foreach (var state in datas)
                    Console.WriteLine(state);
            }
        }
    }
}
