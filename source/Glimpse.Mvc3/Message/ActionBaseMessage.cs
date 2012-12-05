﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using Glimpse.Core.Extensibility; 
using Glimpse.Core.Message;

namespace Glimpse.Mvc.Message
{
    public class ActionBaseMessage : TimelineMessage, IActionBaseMessage
    {
        public ActionBaseMessage(TimerResult timerResult, bool isChildAction, Type executedType, MethodInfo method, string eventName = null, string eventCategory = null)
            : base(timerResult, executedType, method, eventName, eventCategory) 
        {
            IsChildAction = isChildAction;
        }
         
        public bool IsChildAction { get; protected set; }

        public override void BuildDetails(IDictionary<string, object> details)
        {
            base.BuildDetails(details);

            details.Add("IsChildAction", IsChildAction);
            if (ExecutedMethod != null)
            {
                details.Add("ExecutionMethod", ExecutedMethod.Name);
            }

            if (ExecutedType != null)
            {
                details.Add("ExecutedType", ExecutedType);
            }
        }

        protected static bool GetIsChildAction(ControllerBase controller)
        {
            return GetIsChildAction(controller.ControllerContext);
        }

        protected static bool GetIsChildAction(ControllerContext controllerContext)
        {
            return controllerContext != null && controllerContext.IsChildAction;
        }

        protected static string GetActionName(ActionDescriptor descriptor)
        {
            return descriptor.ActionName;
        }

        protected static string GetActionName(ControllerContext controllerContext)
        {
            return GetActionName(controllerContext.Controller);
        }

        protected static string GetActionName(ControllerBase controller)
        {
            // TODO: Fix me!
            return "I'M BROKEN";

            // return controller.ValueProvider.GetValue("action").RawValue.ToStringOrDefault();
        }

        protected static string GetControllerName(ActionDescriptor descriptor)
        {
            return descriptor.ControllerDescriptor.ControllerName;
        }

        protected static string GetControllerName(ControllerContext controllerContext)
        {
            return GetControllerName(controllerContext.Controller);
        }

        protected static string GetControllerName(ControllerBase controller)
        {
            // TODO: Fix me!
            return "I'M BROKEN";

            // return controller.ValueProvider.GetValue("controller").RawValue.ToStringOrDefault();
        }

        protected static Type GetExecutedType(ActionDescriptor descriptor)
        {
            return descriptor.ControllerDescriptor.ControllerType;
        }
    }
}