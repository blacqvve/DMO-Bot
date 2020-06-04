using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BotLib
{
     public  class Utility
     {
          public Utility()
          {

          }
          public IEnumerable<Control> GetAllFormControls(Control control, Type type)
          {
               var controls = control.Controls.Cast<Control>();

               return controls.SelectMany(ctrl => GetAllFormControls(ctrl, type))
                                         .Concat(controls)
                                         .Where(c => c.GetType() == type);
          }
     }
}
