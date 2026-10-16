using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsuarioNoExisteException : Exception
{
    public UsuarioNoExisteException(string msg) : base(msg) { }
}

public class ContraseniaIncorrectaException : Exception
{
    public ContraseniaIncorrectaException(string msg) : base(msg) { }
}

// Error de lectura/escritura del archivo. La capa de datos lo traduce a algo que el presentador entiende.
public class ErrorPersistenciaException : Exception
{
    public ErrorPersistenciaException(string msg, Exception inner) : base(msg, inner) { }
}