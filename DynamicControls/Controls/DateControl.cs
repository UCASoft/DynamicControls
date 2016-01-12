using System;
using System.Linq;
using System.Web.Mvc;

using DynamicControls.Exceptions;

using Newtonsoft.Json.Linq;

namespace DynamicControls.Controls
{
    /// <summary>
    /// The date control.
    /// </summary>
    public class DateControl : InputControl
    {
        /// <summary>
        /// The create control.
        /// </summary>
        /// <returns>
        /// The <see cref="TagBuilder"/>.
        /// </returns>
        protected override TagBuilder CreateControl()
        {
            TagBuilder control = base.CreateControl();
            control.Attributes.Add("type", "date");
            control.Attributes["onchange"] = "dateChange(this)";
            return control;
        }

        /// <summary>
        /// The prepare checked roles.
        /// </summary>
        /// <param name="control">
        /// The control.
        /// </param>
        /// <param name="checkedRoles">
        /// The checked roles.
        /// </param>
        protected override void PrepareCheckedRoles(TagBuilder control, JObject checkedRoles)
        {
            if (checkedRoles["min"] != null)
            {
                control.Attributes.Add("min", CreateRole(checkedRoles.Value<string>("min")));
            }
            if (checkedRoles["max"] != null)
            {
                control.Attributes.Add("max", CreateRole(checkedRoles.Value<string>("max")));
            }
        }

        /// <summary>
        /// The create min role.
        /// </summary>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string CreateRole(string expression)
        {
            DateTime startDate;
            if (expression.StartsWith("now"))
            {
                startDate = DateTime.Now;
            }
            else
            {
                string date = expression.Substring(0, 10);
                int[] dates = date.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Select(d => Convert.ToInt32(d)).ToArray();
                if (dates.Length != 3)
                    throw new InvalidRoleExpression(expression);
                startDate = new DateTime(dates[2], dates[1], dates[0]);
            }
            int addStartIndex = expression.IndexOf("+", StringComparison.Ordinal);
            if (addStartIndex == -1)
                addStartIndex = expression.IndexOf("-", StringComparison.Ordinal);
            if (addStartIndex > -1)
            {
                int direct = expression[addStartIndex] == '+' ? 1 : -1;
                string size = expression.Substring(addStartIndex + 1);
                if (size.EndsWith("days"))
                {
                    startDate = startDate.AddDays(direct * Convert.ToInt32(size.Substring(0, size.Length - 4)));
                }
                else if (size.EndsWith("months"))
                {
                    startDate = startDate.AddMonths(direct * Convert.ToInt32(size.Substring(0, size.Length - 6)));
                }
                else
                {
                    startDate = startDate.AddYears(direct * Convert.ToInt32(size.Substring(0, size.Length - 5)));
                }
            }
            return startDate.ToString("yyyy-MM-dd");
        }
    }
}
