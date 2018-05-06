using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgeingHaresSimulator.Common.UI
{
    internal sealed class LogisticFunctionUiEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if ((context != null) && (provider != null))
            {
                LogisticFunction function = value as LogisticFunction;
                if (function != null)
                {
                    using (LogisticFunctionEditorForm form = new LogisticFunctionEditorForm(function))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            return form.Value;
                        }
                    }
                }
            }

            return (value);
        }
    }
}
