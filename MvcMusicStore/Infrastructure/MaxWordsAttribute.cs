﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcMusicStore.Infrastructure
{
    public class MaxWordsAttribute : ValidationAttribute, IClientValidatable
    {
        public MaxWordsAttribute(int maxWords) : base("{0} has too many words!")
        {
            _maxWords = maxWords;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var valueAsString = value.ToString();
                if (valueAsString.Split(' ').Length > _maxWords)
                {
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errorMessage);
                }
            }
            return ValidationResult.Success;
        }
        public int _maxWords { get; set; }

        public IEnumerable<ModelClientValidationRule> 
            GetClientValidationRules(ModelMetadata metadata, 
            ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage =
                FormatErrorMessage(metadata.GetDisplayName());
            rule.ValidationParameters.Add("wordcount", _maxWords);
            rule.ValidationType = "maxwords";
            yield return rule;
        }

        //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //{
        //    //return base.IsValid(value, validationContext);
        //    ValidationResult result = new ValidationResult("Invalid");
        //    return result;
        //}
    }
}