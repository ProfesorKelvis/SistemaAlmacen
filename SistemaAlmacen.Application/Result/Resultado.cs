namespace SistemaAlmacen.Application.Result
{
    public class Resultado<T>
    {
        public bool IsSuccess { get; }
        public int Codigo { get; }
        public string Error { get; }
        public T? Data { get; }

        private Resultado(bool isSuccess, T? value, string error, int code)
        {
            IsSuccess = isSuccess;
            Data = value;
            Error = error;
            Codigo = code;
        }

        public static Resultado<T> Success(T value) => new(true, value, "", 200);
        public static Resultado<T> Failure(string error) => new(false, default, error, 400);

    }
}