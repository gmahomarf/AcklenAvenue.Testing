// Copyright (c) Microsoft Corporation.  All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;
using AcklenAvenue.Expressions;

namespace AcklenAvenue.Testing.Moq
{
    /// <summary>
    /// Compare two expressions to determine if they are equivalent
    /// </summary>
    public class ExpressionComparer
    {
        readonly Func<object, object, bool> _fnCompare;
        ScopedDictionary<ParameterExpression, ParameterExpression> _parameterScope;

        protected ExpressionComparer(
            ScopedDictionary<ParameterExpression, ParameterExpression> parameterScope,
            Func<object, object, bool> fnCompare
            )
        {
            _parameterScope = parameterScope;
            _fnCompare = fnCompare;
        }

        protected Func<object, object, bool> FnCompare
        {
            get { return _fnCompare; }
        }

        public static bool AreEqual(Expression expected, Expression actual)
        {
            return AreEqual(null, expected, actual);
        }

        public static bool AreEqual(Expression expected, Expression actual, Func<object, object, bool> fnCompare)
        {
            return AreEqual(null, expected, actual, fnCompare);
        }

        public static bool AreEqual(ScopedDictionary<ParameterExpression, ParameterExpression> parameterScope,
                                    Expression expected, Expression actual)
        {
            return new ExpressionComparer(parameterScope, null).Compare(expected, actual);
        }

        public static bool AreEqual(ScopedDictionary<ParameterExpression, ParameterExpression> parameterScope,
                                    Expression expected, Expression actual, Func<object, object, bool> fnCompare)
        {
            return new ExpressionComparer(parameterScope, fnCompare).Compare(expected, actual);
        }

        protected virtual bool Compare(Expression expected, Expression actual)
        {
            if (expected == actual)
                return true;
            if (expected == null || actual == null)
                return false;
            if (expected.NodeType != actual.NodeType)
                return false;
            if (expected.Type != actual.Type)
                return false;
            switch (expected.NodeType)
            {
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                case ExpressionType.Not:
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.ArrayLength:
                case ExpressionType.Quote:
                case ExpressionType.TypeAs:
                case ExpressionType.UnaryPlus:
                    return CompareUnary((UnaryExpression) expected, (UnaryExpression) actual);
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.Coalesce:
                case ExpressionType.ArrayIndex:
                case ExpressionType.RightShift:
                case ExpressionType.LeftShift:
                case ExpressionType.ExclusiveOr:
                case ExpressionType.Power:
                    return CompareBinary((BinaryExpression) expected, (BinaryExpression) actual);
                case ExpressionType.TypeIs:
                    return CompareTypeIs((TypeBinaryExpression) expected, (TypeBinaryExpression) actual);
                case ExpressionType.Conditional:
                    return CompareConditional((ConditionalExpression) expected, (ConditionalExpression) actual);
                case ExpressionType.Constant:
                    return CompareConstant((ConstantExpression) expected, (ConstantExpression) actual);
                case ExpressionType.Parameter:
                    return CompareParameter((ParameterExpression) expected, (ParameterExpression) actual);
                case ExpressionType.MemberAccess:
                    return CompareMemberAccess((MemberExpression) expected, (MemberExpression) actual);
                case ExpressionType.Call:
                    return CompareMethodCall((MethodCallExpression) expected, (MethodCallExpression) actual);
                case ExpressionType.Lambda:
                    return CompareLambda((LambdaExpression) expected, (LambdaExpression) actual);
                case ExpressionType.New:
                    return CompareNew((NewExpression) expected, (NewExpression) actual);
                case ExpressionType.NewArrayInit:
                case ExpressionType.NewArrayBounds:
                    return CompareNewArray((NewArrayExpression) expected, (NewArrayExpression) actual);
                case ExpressionType.Invoke:
                    return CompareInvocation((InvocationExpression) expected, (InvocationExpression) actual);
                case ExpressionType.MemberInit:
                    return CompareMemberInit((MemberInitExpression) expected, (MemberInitExpression) actual);
                case ExpressionType.ListInit:
                    return CompareListInit((ListInitExpression) expected, (ListInitExpression) actual);
                default:
                    throw new Exception(string.Format("Unhandled expression type: '{0}'", expected.NodeType));
            }
        }

        protected virtual bool CompareUnary(UnaryExpression expected, UnaryExpression actual)
        {
            if ((expected).NodeType != (actual).NodeType)
            {
                throw new Exception(
                    string.Format(FormatErrorMessage("'NodeType' of part of the expression didn't match.",
                                                     (expected).NodeType.ToString(), (actual).NodeType.ToString(),
                                                     expected, actual)));
            }
            if (expected.Method != actual.Method)
            {
                throw new Exception(
                    string.Format(FormatErrorMessage("'Method' of part of the expression didn't match.",
                                                     expected.Method.ToString(), actual.Method.ToString(), expected,
                                                     actual)));
            }
            if (expected.IsLifted != actual.IsLifted)
            {
                throw new Exception(
                    string.Format(FormatErrorMessage("'IsLifted' of part of the expression didn't match.",
                                                     expected.IsLifted.ToString(), actual.IsLifted.ToString(), expected,
                                                     actual)));
            }
            if (expected.IsLiftedToNull != actual.IsLiftedToNull)
            {
                throw new Exception(
                    string.Format(FormatErrorMessage("'IsLiftedToNull' of part of the expression didn't match.",
                                                     expected.IsLiftedToNull.ToString(),
                                                     actual.IsLiftedToNull.ToString(), expected, actual)));
            }
            if (!Compare(expected.Operand, actual.Operand))
            {
                throw new Exception(
                    string.Format(FormatErrorMessage("'Operand' of part of the expression didn't match.",
                                                     expected.Operand, actual.Operand, expected, actual)));
            }
            return true;
        }

        protected virtual bool CompareBinary(BinaryExpression expected, BinaryExpression actual)
        {
            if (expected.NodeType != actual.NodeType)
            {
                throw new Exception(
                    string.Format(FormatErrorMessage("'NodeType' of part of the expression didn't match.",
                                                     expected.NodeType.ToString(), actual.NodeType.ToString(), expected,
                                                     actual)));
            }
            if (expected.Method != actual.Method)
            {
                throw new Exception(
                    string.Format(FormatErrorMessage("'Method' of part of the expression didn't match.",
                                                     expected.Method.ToString(), actual.Method.ToString(), expected,
                                                     actual)));
            }
            if (expected.IsLifted != actual.IsLifted)
            {
                throw new Exception(
                    string.Format(FormatErrorMessage("'IsLifted' of part of the expression didn't match.",
                                                     expected.IsLifted.ToString(), actual.IsLifted.ToString(), expected,
                                                     actual)));
            }
            if (expected.IsLiftedToNull != actual.IsLiftedToNull)
            {
                throw new Exception(
                    string.Format(FormatErrorMessage("'IsLiftedToNull' of part of the expression didn't match.",
                                                     expected.IsLiftedToNull.ToString(),
                                                     actual.IsLiftedToNull.ToString(), expected, actual)));
            }
            if (!Compare(expected.Left, actual.Left))
            {
                throw new Exception(
                    string.Format(FormatErrorMessage("Left sides of part of the expression didn't match.",
                                                     expected.Left, actual.Left, expected, actual)));
            }
            if (!Compare(expected.Right, actual.Right))
            {
                string formatErrorMessage = FormatErrorMessage("Right sides of part of the expression didn't match.",
                                                               expected.Right, actual.Right, expected, actual);

                formatErrorMessage +=
                    "\r\n\r\nNOTE: Sometimes this can happen because you have used a variable in your test to populate the value of a part of the expression. Private variables can't be compared for some reason. However, if you change the variable to a constant, it might work.";

                throw new Exception(
                    string.Format(formatErrorMessage));
            }
            return true;
        }

        string FormatErrorMessage(string message, params object[] data)
        {
            const string supportingInfo =
                "\r\nExpected: {0}\r\nActual: {1}\r\n\r\nExpected Expression: {2}\r\nActual Expression: {3}";
            return string.Format(message + supportingInfo, data);
        }

        protected virtual bool CompareTypeIs(TypeBinaryExpression expected, TypeBinaryExpression actual)
        {
            return expected.TypeOperand == actual.TypeOperand
                   && Compare(expected.Expression, actual.Expression);
        }

        protected virtual bool CompareConditional(ConditionalExpression expected, ConditionalExpression actual)
        {
            return Compare(expected.Test, actual.Test)
                   && Compare(expected.IfTrue, actual.IfTrue)
                   && Compare(expected.IfFalse, actual.IfFalse);
        }

        protected virtual bool CompareConstant(ConstantExpression expected, ConstantExpression actual)
        {
            if (_fnCompare != null)
            {
                return _fnCompare(expected.Value, actual.Value);
            }
            else
            {
                return Equals(expected.Value, actual.Value);
            }
        }

        protected virtual bool CompareParameter(ParameterExpression expected, ParameterExpression actual)
        {
            if (_parameterScope != null)
            {
                ParameterExpression mapped;
                if (_parameterScope.TryGetValue(expected, out mapped))
                    return mapped == actual;
            }
            return expected == actual;
        }

        protected virtual bool CompareMemberAccess(MemberExpression expected, MemberExpression actual)
        {
            return expected.Member == actual.Member
                   && Compare(expected.Expression, actual.Expression);
        }

        protected virtual bool CompareMethodCall(MethodCallExpression expected, MethodCallExpression actual)
        {
            return expected.Method == actual.Method
                   && Compare(expected.Object, actual.Object)
                   && CompareExpressionList(expected.Arguments, actual.Arguments);
        }

        protected virtual bool CompareLambda(LambdaExpression expected, LambdaExpression actual)
        {
            int n = expected.Parameters.Count;
            if (actual.Parameters.Count != n)
                return false;
            // all must have same type
            for (int i = 0; i < n; i++)
            {
                if ((expected.Parameters[i]).Type != (actual.Parameters[i]).Type)
                    return false;
            }
            ScopedDictionary<ParameterExpression, ParameterExpression> save = _parameterScope;
            _parameterScope = new ScopedDictionary<ParameterExpression, ParameterExpression>(_parameterScope);
            try
            {
                for (int i = 0; i < n; i++)
                {
                    _parameterScope.Add(expected.Parameters[i], actual.Parameters[i]);
                }
                return Compare(expected.Body, actual.Body);
            }
            finally
            {
                _parameterScope = save;
            }
        }

        protected virtual bool CompareNew(NewExpression expected, NewExpression actual)
        {
            return expected.Constructor == actual.Constructor
                   && CompareExpressionList(expected.Arguments, actual.Arguments)
                   && CompareMemberList(expected.Members, actual.Members);
        }

        protected virtual bool CompareExpressionList(ReadOnlyCollection<Expression> expected,
                                                     ReadOnlyCollection<Expression> actual)
        {
            if (expected == actual)
                return true;
            if (expected == null || actual == null)
                return false;
            if (expected.Count != actual.Count)
                return false;
            for (int i = 0, n = expected.Count; i < n; i++)
            {
                if (!Compare(expected[i], actual[i]))
                    return false;
            }
            return true;
        }

        protected virtual bool CompareMemberList(ReadOnlyCollection<MemberInfo> expected,
                                                 ReadOnlyCollection<MemberInfo> actual)
        {
            if (expected == actual)
                return true;
            if (expected == null || actual == null)
                return false;
            if (expected.Count != actual.Count)
                return false;
            for (int i = 0, n = expected.Count; i < n; i++)
            {
                if (expected[i] != actual[i])
                    return false;
            }
            return true;
        }

        protected virtual bool CompareNewArray(NewArrayExpression expected, NewArrayExpression actual)
        {
            return CompareExpressionList(expected.Expressions, actual.Expressions);
        }

        protected virtual bool CompareInvocation(InvocationExpression expected, InvocationExpression actual)
        {
            return Compare(expected.Expression, actual.Expression)
                   && CompareExpressionList(expected.Arguments, actual.Arguments);
        }

        protected virtual bool CompareMemberInit(MemberInitExpression expected, MemberInitExpression actual)
        {
            return Compare(expected.NewExpression, actual.NewExpression)
                   && CompareBindingList(expected.Bindings, actual.Bindings);
        }

        protected virtual bool CompareBindingList(ReadOnlyCollection<MemberBinding> expected,
                                                  ReadOnlyCollection<MemberBinding> actual)
        {
            if (expected == actual)
                return true;
            if (expected == null || actual == null)
                return false;
            if (expected.Count != actual.Count)
                return false;
            for (int i = 0, n = expected.Count; i < n; i++)
            {
                if (!CompareBinding(expected[i], actual[i]))
                    return false;
            }
            return true;
        }

        protected virtual bool CompareBinding(MemberBinding expected, MemberBinding actual)
        {
            if (expected == actual)
                return true;
            if (expected == null || actual == null)
                return false;
            if (expected.BindingType != actual.BindingType)
                return false;
            if (expected.Member != actual.Member)
                return false;
            switch (expected.BindingType)
            {
                case MemberBindingType.Assignment:
                    return CompareMemberAssignment((MemberAssignment) expected, (MemberAssignment) actual);
                case MemberBindingType.ListBinding:
                    return CompareMemberListBinding((MemberListBinding) expected, (MemberListBinding) actual);
                case MemberBindingType.MemberBinding:
                    return CompareMemberMemberBinding((MemberMemberBinding) expected, (MemberMemberBinding) actual);
                default:
                    throw new Exception(string.Format("Unhandled binding type: '{0}'", expected.BindingType));
            }
        }

        protected virtual bool CompareMemberAssignment(MemberAssignment expected, MemberAssignment actual)
        {
            return expected.Member == actual.Member
                   && Compare(expected.Expression, actual.Expression);
        }

        protected virtual bool CompareMemberListBinding(MemberListBinding expected, MemberListBinding actual)
        {
            return expected.Member == actual.Member
                   && CompareElementInitList(expected.Initializers, actual.Initializers);
        }

        protected virtual bool CompareMemberMemberBinding(MemberMemberBinding expected, MemberMemberBinding actual)
        {
            return expected.Member == actual.Member
                   && CompareBindingList(expected.Bindings, actual.Bindings);
        }

        protected virtual bool CompareListInit(ListInitExpression expected, ListInitExpression actual)
        {
            return Compare(expected.NewExpression, actual.NewExpression)
                   && CompareElementInitList(expected.Initializers, actual.Initializers);
        }

        protected virtual bool CompareElementInitList(ReadOnlyCollection<ElementInit> expected,
                                                      ReadOnlyCollection<ElementInit> actual)
        {
            if (expected == actual)
                return true;
            if (expected == null || actual == null)
                return false;
            if (expected.Count != actual.Count)
                return false;
            for (int i = 0, n = expected.Count; i < n; i++)
            {
                if (!CompareElementInit(expected[i], actual[i]))
                    return false;
            }
            return true;
        }

        protected virtual bool CompareElementInit(ElementInit expected, ElementInit actual)
        {
            return expected.AddMethod == actual.AddMethod
                   && CompareExpressionList(expected.Arguments, actual.Arguments);
        }
    }
}