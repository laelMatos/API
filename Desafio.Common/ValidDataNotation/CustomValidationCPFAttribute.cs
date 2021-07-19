﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Desafio.Common
{
    /// <summary>
    /// Validação customizada para CPF
    /// </summary>
    public class CustomValidationCPFAttribute : ValidationAttribute, IClientValidatable
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public CustomValidationCPFAttribute() { }

        /// <summary>
        /// Validação server
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            //Se não tiver valor ignora a validação
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return true;

            bool valido = AssertionConcern.ValidaCPF(value.ToString());
            return valido;
        }

        /// <summary>
        /// Validação client
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.FormatErrorMessage(null),
                ValidationType = "customvalidationcpf"
            };
        }
    }
}
