using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTracker.Data.Models.Responses
{
    public class RequestResult
    {
        private bool success;

        public bool IsSuccess
        {
            get { return success && (Errors == null || Errors.Count == 0); }
            protected set { success = value; }
        }

        public IDictionary<string, string> Errors { get; protected set; }

        public Exception Exception { get; protected set; }

        public string Title { get; protected set; }

        public static RequestResult Error()
        {
            return new RequestResult
            {
                IsSuccess = false
            };
        }

        public static RequestResult Error(string error)
        {
            return new RequestResult()
            {
                IsSuccess = false,
                Title = error
            };
        }

        public static RequestResult Error(string error, string errorType, Exception ex = null)
        {
            return new RequestResult()
            {
                IsSuccess = false,
                Errors = new Dictionary<string, string> { { errorType, error } },
                Exception = ex
            };
        }

        public static RequestResult Error(IDictionary<string, string> errors)
        {
            return new RequestResult()
            {
                IsSuccess = false,
                Errors = errors,
            };
        }

        public static RequestResult Error(Exception ex)
        {
            return new RequestResult
            {
                IsSuccess = false,
                Exception = ex
            };
        }

        public static RequestResult<T> Error<T>(string error, Exception ex = null)
        {
            return new RequestResult<T>(default(T))
            {
                IsSuccess = false,
                Title = error,
                Exception = ex
            };
        }

        public static RequestResult<T> Error<T>(string error, string errorType, Exception ex = null)
        {
            return new RequestResult<T>(default(T))
            {
                IsSuccess = false,
                Errors = new Dictionary<string, string> { { errorType, error } },
                Exception = ex
            };
        }

        public static RequestResult<T> Error<T>(string error, string errorType, string title, Exception ex = null)
        {
            return new RequestResult<T>(default(T))
            {
                IsSuccess = false,
                Errors = new Dictionary<string, string> { { errorType, error } },
                Exception = ex,
                Title = title
            };
        }

        public static RequestResult<T> Error<T>()
        {
            return new RequestResult<T>(default(T))
            {
                IsSuccess = false
            };
        }

        public static RequestResult<T> Error<T>(Exception ex)
        {
            return new RequestResult<T>(default(T))
            {
                IsSuccess = false,
                Exception = ex
            };
        }

        public static RequestResult<T> Error<T>(IDictionary<string, string> errors)
        {
            return new RequestResult<T>(default(T))
            {
                IsSuccess = false,
                Errors = errors,
            };
        }


        public static RequestResult Success()
        {
            return new RequestResult()
            {
                IsSuccess = true
            };
        }

        public static RequestResult<T> Success<T>(T result)
        {
            return new RequestResult<T>(result)
            {

                IsSuccess = true
            };
        }

        public static RequestResult<T> Success<T>()
        {
            return new RequestResult<T>()
            {
                IsSuccess = true
            };

        }
    }



    public class RequestResult<T> : RequestResult
    {
        public RequestResult(T result)
        {
            Obj = result;
        }

        public RequestResult() { }

        public T Obj { get; }

    }
}
