using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;

using DevExpress.XtraEditors;

namespace HKInc.Service.Helper
{
    public static class FormHelper
    {
        public static XtraForm CreateForm(string assemblyName, string namespaceName, string instanceName)
        {
            try
            {
                if (String.IsNullOrEmpty(assemblyName) || String.IsNullOrEmpty(namespaceName) || String.IsNullOrEmpty(instanceName))
                    return null;

                Assembly assembly = null;
                if (Assembly.GetExecutingAssembly().Equals(Assembly.LoadFrom(@"" + assemblyName)))
                    assembly = Assembly.GetExecutingAssembly();
                else
                    assembly = Assembly.LoadFrom(String.Format(@"{0}\\{1}", Application.StartupPath, assemblyName));

                return (XtraForm)assembly.CreateInstance(String.Format("{0}.{1}", namespaceName, instanceName));
            }
            catch (Exception ex)
            {
                throw new Exception((new Handler.ExceptionHandler()).GetMessage(ex));
            }
        }

        public static bool IsLoadedForm(string formName)
        {
            if (Application.OpenForms.Count > 0)
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (form.Name.Equals(formName))
                        return true;
                }
            }
            return false;
        }

        public static Form GetParentForm(string formName)
        {
            try
            {
                return Application.OpenForms[formName];
            }
            catch
            {
                return null;
            }
        }
    }    
}
