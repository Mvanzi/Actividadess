using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public class RespuestaAPI { public UsuarioData[] results; }
[Serializable] public class UsuarioData { public NameData name; public LocationData location; public string email; public LoginData login; public DobData dob; public string phone; public PictureData picture; }
[Serializable] public class NameData { public string first; public string last; }
[Serializable] public class LocationData { public StreetData street; public string city; public string state; public string country; }
[Serializable] public class StreetData { public int number; public string name; }
[Serializable] public class LoginData { public string username; public string password; }
[Serializable] public class DobData { public int age; }
[Serializable] public class PictureData { public string large; }