var map = await File.ReadAllLinesAsync(args[0]);
var width = map.Select(o => o.Length).Max();
var height = map.Length;

