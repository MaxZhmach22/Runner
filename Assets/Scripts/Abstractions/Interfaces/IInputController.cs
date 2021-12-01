using UniRx;


namespace Runner
{
    internal interface IInputController
    {
        Subject<Directions> DirectionToMove { get; }
    }
}