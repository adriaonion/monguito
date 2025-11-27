using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Nota
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Titulo { get; set; }

    public string Contenido { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime FechaCreacion { get; set; }

    public List<string> Tags { get; set; } = new List<string>();
}