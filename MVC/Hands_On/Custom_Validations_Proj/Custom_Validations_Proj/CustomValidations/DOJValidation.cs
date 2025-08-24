using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Custom_Validations_Proj.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace Custom_Validations_Proj.CustomValidations
{

    public class DOJValidate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || !(value is DateTime doj))
            {
                return false;
            }

            return doj <= DateTime.Today;
        }
    }

}