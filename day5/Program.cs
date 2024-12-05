var lines = await File.ReadAllLinesAsync(args[0]);

List<(int x, int y)> pageOrderingRules =
    (from line in lines.TakeWhile(o => o.Length > 0)
     let separatorIndex = line.IndexOf('|')
     let x = int.Parse(line.Substring(0, separatorIndex))
     let y = int.Parse(line.Substring(separatorIndex + 1, line.Length - (separatorIndex + 1)))
     select (x, y)).ToList();

List<List<int>> pagesToProduceInEachUpdate =
    (from line in lines.Skip(pageOrderingRules.Count + 1)
     select line.Split(',').Select(int.Parse).ToList()).ToList();

