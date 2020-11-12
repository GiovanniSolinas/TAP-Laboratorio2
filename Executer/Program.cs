using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MyAttribute;

namespace Executer
{
    class Program
    {
        static void Main(string[] args)
        {
            var libraryToBeExecutedAssembly = Assembly.LoadFrom(@"../../../MyLibrary/bin/Debug/MyLibrary.dll");
            foreach (var type in libraryToBeExecutedAssembly.GetTypes())
                if (type.IsClass)
                {
                    Console.WriteLine($"Tipo: {type.FullName}");
                    var classMethods = type.GetMethods();
                    foreach (var methodToBeExecuted in classMethods)
                    {
                        var executions = methodToBeExecuted.GetCustomAttributes < ExecuteMeAttribute>();
                        foreach (var oneExecution in executions)
                        {
                            if (!ParametersMatch(methodToBeExecuted.GetParameters(), oneExecution.Arguments, out var error))
                            {
                                Console.WriteLine($"{methodToBeExecuted.Name} cannot be executed: " + error);
                                continue;
                            }
                            object receiver = null;
                            if (!methodToBeExecuted.IsStatic)
                                if (DefaultConstructorExists(type))
                                    receiver = Activator.CreateInstance(type);
                                else
                                {
                                    Console.WriteLine(
                                        $"Execution of method {methodToBeExecuted.Name} requires an object but type {type} does not have default constructor");
                                    break;
                                }
                            methodToBeExecuted.Invoke(receiver, oneExecution.Arguments);
                        }
                    }
                }
            Console.ReadLine();

            bool ParametersMatch(ParameterInfo[] formalParameters, object[] actualParameters, out string errorMessage)
            {
                if (null == formalParameters)
                    throw new ArgumentNullException($"{nameof(formalParameters)} cannot be null");
                if (null == actualParameters)
                    throw new ArgumentNullException($"{nameof(actualParameters)} cannot be null");
                if (formalParameters.Length != actualParameters.Length)
                {
                    errorMessage = $"Expected {formalParameters.Length}, but {actualParameters.Length} were provided";
                    return false;
                }

                for (int i = 0; i < formalParameters.Length; i++)
                {
                    //Per i sottotipi. es: int è sottotipo di float e se passo un int in un metodo che richiede float devo poter esegurirlo
                    if (!formalParameters[i].ParameterType.IsInstanceOfType(actualParameters[i])) 
                    {
                        errorMessage = $"Expected {i}-th parameter of type {formalParameters[i].ParameterType.FullName}, but the actual parameter provided has type {actualParameters[i].GetType().FullName}";
                        return false;
                    }
                }
                errorMessage = "";
                return true;
            }

            bool DefaultConstructorExists(Type type)
            {
                if (null == type)
                    throw new ArgumentNullException("null type cannot be managed");
                var constructors = type.GetConstructors();
                foreach (var constructor in constructors)
                    if (constructor.GetParameters().Length == 0)
                        return true;
                return false;
            }

        }
    }
            
            
}

