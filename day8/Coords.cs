namespace day8;
public readonly record struct Coords(int X, int Y) {
    public override string ToString()
    {
        return $"{X},{Y}";
    }
}
