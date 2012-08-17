using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace ExpressionExperiments
{
	class Program
	{
		static void Main(string[] args)
		{
			var expression = Simple();
			PrintExpression(expression);
			InspectExpression(expression);

			Console.ReadLine();
		}

		static Expression Simple()
		{
			// “(jobs or steve) and apple”

			var jobs = Expression.Parameter(typeof(bool), "jobs");
			var steve = Expression.Parameter(typeof(bool), "steve");
			var apple = Expression.Parameter(typeof(bool), "apple");

			var or = Expression.Or(jobs, steve);

			var and = Expression.And(or, apple);

			return and;
		}

		static Expression Evaluatable()
		{
			// “(jobs or steve) and apple”

			var x = Expression.Parameter(typeof(string), "x");

			var jobs = Expression.Equal(x, Expression.Constant("jobs"));
			var steve = Expression.Equal(x, Expression.Constant("steve"));
			var apple = Expression.Equal(x, Expression.Constant("apple"));

			var or = Expression.Or(jobs, steve);

			var and = Expression.And(or, apple);

			return and;
		}

		static void PrintExpression(Expression expression)
		{
			Console.WriteLine(expression.ToString());
		}

		static void InspectExpression(Expression expression)
		{
			var type = expression.NodeType;

			switch (type)
			{
				case ExpressionType.Constant:
					var constant = expression as ConstantExpression;
					Console.WriteLine("found a keyword: " + constant.Value);
					break;
				case ExpressionType.And:
				case ExpressionType.Or:
					var binary = expression as BinaryExpression;
					var left = binary.Left;
					var right = binary.Right;
					Console.WriteLine(
						string.Format("found operator {0} between operands {1} and {2}.",
							binary.NodeType, binary.Left, binary.Right));
					InspectExpression(left);
					InspectExpression(right);
					break;
				case ExpressionType.Equal:
					binary = expression as BinaryExpression;
					left = binary.Left;
					right = binary.Right;
					Console.WriteLine(
						string.Format("found operator {0} between operands {1} and {2}.",
							binary.NodeType, binary.Left, binary.Right));
					InspectExpression(left);
					InspectExpression(right);
					break;
				default:
					break;
			}
		}
	}
}
