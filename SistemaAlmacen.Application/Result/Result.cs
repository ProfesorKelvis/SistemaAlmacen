namespace SistemaAlmacen.Application.Result
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public int Codigo { get; }
        public string Error { get; }
        public T? Data { get; }

        private Result(bool isSuccess, T? value, string error, int code)
        {
            IsSuccess = isSuccess;
            Data = value;
            Error = error;
            Codigo = code;
        }

        public static Result<T> Success(T value) => new(true, value, "", 200);
        public static Result<T> Failure(string error) => new(false, default, error, 400);


    }
}