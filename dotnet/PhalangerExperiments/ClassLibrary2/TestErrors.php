<?
	namespace Phalanger
	{
        use System as S;
        use System\Collections\Generic as G;
	
		[\Export]
		class TestErrors
		{
			[\Export]
			static function GenericMethod<:T = string:>(T $arg1)
			{
				$dict = new G\Dictionary<:string, T:>;
				$dict->Add("test", $arg1);
				
				return $dict;
			}
		
			[\Export]
			static function SomeMethodWithSyntaxError<:T = string:>(T $arg1)
			{
				$dict = new G\Dictionary<:string, T:>;
				$dict->Ad("test", $arg1);
				
				return $dict;
			}
		
			[\Export]
			static function SomeMethodWithLogicalError<:T = string:>(T $arg1)
			{
				$array = array(1, 2, 3);
				return $array[100];
			}
		
			[\Export]
			static function SomeMethodWithTriggerError<:T = string:>(T $arg1)
			{
				$dict = new G\Dictionary<:string, T:>;
				$dict->Add(1, $arg1);
				trigger_error("This is an explicit error using trigger_error");
				return $dict;
			}
		
			[\Export]
			static function SomeMethodWithDie<:T = string:>(T $arg1)
			{
				$dict = new G\Dictionary<:string, T:>;
				$dict->Add(1, $arg1);
				die("This is an explicit error using die");
				return $dict;
			}
		
			[\Export]
			static function SomeMethodWithException<:T = string:>(T $arg1)
			{
				throw new Exception("This is an explicit exception");
			}
		
			[\Export]
			static function SomeMethodWithRuntimeException<:T = int:>(T $arg1)
			{				
				return 1/$arg1;
			}
		}
	}
?>
