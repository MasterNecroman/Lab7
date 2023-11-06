using System;

namespace task_1
{
    public class Calculator<T>
    {
        public delegate T OperationDelegate(T a, T b);

        public OperationDelegate Addition { get; set; }
        public OperationDelegate Subtraction { get; set; }
        public OperationDelegate Multiplication { get; set; }
        public OperationDelegate Division { get; set; }

        public Calculator(OperationDelegate addition, OperationDelegate subtraction, OperationDelegate multiplication, OperationDelegate division)
        {
            Addition = addition;
            Subtraction = subtraction;
            Multiplication = multiplication;
            Division = division;
        }

        public T PerformOperation(T a, T b, OperationDelegate operation)
        {
            return operation(a, b);
        }
    }
}