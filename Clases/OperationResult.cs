using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaGestionEstudiantes
{
    public class OperationResult
    {
        // Propiedades requeridas
        public string Message { get; set; }
        public bool Success { get; set; }
        public dynamic Data { get; set; }

        // Constructor principal
        public OperationResult(bool success, string message, dynamic data = null!)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}
        