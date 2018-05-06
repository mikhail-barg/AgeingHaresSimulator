using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgeingHaresSimulator.UI
{
    internal sealed class NormalDistributionUiEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if ((context != null) && (provider != null))
            {
                NormalDistribution distribution = value as NormalDistribution;
                if (distribution != null)
                {
                    using (NormalDistributionEditorForm form = new NormalDistributionEditorForm(distribution))
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
