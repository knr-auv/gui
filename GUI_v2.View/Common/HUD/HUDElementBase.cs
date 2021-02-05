using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GUI_v2.View.Common.HUD
{
    public class HUDElementBase
    {
        protected int _x;
        protected int _y;
        protected int _width;
        protected int _height;
        protected Canvas parent;
        public HUDElementBase(int x, int y, int width, int height, Canvas parent)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            this.parent = parent;
        }

        public void Resize(int x, int y, int width, int height)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
        }

        public double[] GridToCnv()
        {
            double[] ret = new double[4];
            ret[0] = _x * parent.ActualWidth / 100;
            ret[1] = _y * parent.ActualHeight / 100;
            ret[2] = parent.ActualWidth / 100 * _width;
            ret[3] = parent.ActualHeight / 100 * _height;
            return ret;
        }
    }
}
