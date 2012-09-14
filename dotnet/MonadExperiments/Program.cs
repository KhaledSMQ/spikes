using System;
using IdentityMonad = MonadExperiments.IdentityMonadExtensions;

namespace MonadExperiments
{
	class Program
	{
		static void Main(string[] args)
		{
			var p = new Program();
			p.UseIdentityMonad();
			p.UseMaybeMonad();
			Console.ReadLine();
		}

		private void UseIdentityMonad()
		{
			Console.WriteLine("Using IdentityMonad:");
			var mx1 = 10.IdentityUnit();
			var my1 = 20.IdentityUnit();
			var r1 = mx1.Bind(
				s1 => my1.Bind(
					t1 => (s1 + t1).IdentityUnit()));
			Console.WriteLine("{0} + {1} = {2}", mx1.Value, my1.Value, r1.Value);
	
			var mx2 = 5.IdentityUnit();
			var my2 = 7.IdentityUnit();
			var r2 = mx2.Bind(
				s2 => my2.Bind(
					t2 => (s2 + t2).IdentityUnit()));
			Console.WriteLine("{0} + {1} = {2}", mx2.Value, my2.Value, r2.Value);

			var mx3 = IdentityMonad.Unit(11);
			var my3 = IdentityMonad.Unit(13);
			var r3 = mx3.Bind(
				s3 => my3.Bind(
					t3 => IdentityMonad.Unit(s3 + t3)));
			Console.WriteLine("{0} + {1} = {2}", mx3.Value, my3.Value, r3.Value);
	
		}

		private void UseMaybeMonad()
		{
			Console.WriteLine("Using MaybeMonad:");

			var mx = "x".MaybeUnit();
			var my = 17.MaybeUnit();
			var mz = (new DateTime(2000, 1, 2, 3, 5, 6)).MaybeUnit();
			var mw = Maybe<object>.Nothing;
			
			var r1 = mx.Bind(
				s1 => my.Bind(
					t1 => (string.Format("x = '{0}', y = '{1}'", mx.Value, my.Value)).MaybeUnit())
				);
			Console.WriteLine("r1 = '{0}'", r1.Value);

			var r2 = mx.Bind(
				s2 => my.Bind(
					t2 => mz.Bind(
						u2 => (string.Format("x = '{0}', y = '{1}', z = '{2}'", mx.Value, my.Value, mz.Value)).MaybeUnit())
					)
				);
			Console.WriteLine("r2 = '{0}'", r2.Value);

			var r3 = mx.Bind(
				s3 => mw.Bind(
					t3 => mz.Bind(
						u3 => (string.Format("x = '{0}', w = '{1}', z = '{2}'", mx.Value, mw.Value, mz.Value)).MaybeUnit())
					)
				);
			Console.WriteLine("r3.HasValue = '{0}', r3 = '{1}'", r3.HasValue, r3.Value);
		}
	}
}
