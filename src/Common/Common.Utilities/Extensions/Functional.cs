namespace Common.Utilities.Extensions;

public static class FunctionalExtensions
{
    /// <summary>
    /// Executes a given action depending on a specified condition.
    /// </summary>
    /// <param name="condition"><c>bool</c> condition to be evaluated.</param>
    /// <param name="ifTrue">The <see cref="Action"/> to be executed if the condition is <c>true</c>.</param>
    /// <param name="ifFalse">The <see cref="Action"/> to be executed if the condition is <c>false</c>.</param>
    public static void IfElse(this bool condition, Action ifTrue, Action ifFalse)
    {
        if (condition)
        {
            ifTrue();
        }
        else
        {
            ifFalse();
        }
    }

    public static T IfElse<T>(this bool condition, Func<T> ifTrue, Func<T> ifFalse)
    {
        return condition ? ifTrue() : ifFalse();
    }

    /// <summary>
    /// Executes a given action depending on a specified condition.
    /// </summary>
    /// <param name="condition"><c>bool</c> condition to be evaluated.</param>
    /// <param name="ifTrue">The <see cref="Action"/> to be executed if the condition is <c>true</c>.</param>
    /// <param name="ifFalse">The <see cref="Action"/> to be executed if the condition is <c>false</c>.</param>
    public static async Task IfElse(this bool condition, Task ifTrue, Task ifFalse)
    {
        if (condition)
        {
            await ifTrue;
        }
        else
        {
            await ifFalse;
        }
    }

    /// <summary>
    /// Executes a given action depending on a specified condition.
    /// </summary>
    /// <param name="condition">The <see cref="Task{TResult}"/>, where <c>TResult</c> is <c>bool</c>, to be evaluated.</param>
    /// <param name="ifTrue">The <see cref="Action"/> to be executed if the condition is <c>true</c>.</param>
    /// <param name="ifFalse">The <see cref="Action"/> to be executed if the condition is <c>false</c>.</param>
    public static async Task IfElse(this Task<bool> condition, Action ifTrue, Action ifFalse)
    {
        if (await condition)
        {
            ifTrue();
        }
        else
        {
            ifFalse();
        }
    }

    // TODO: method to imitate switch statement
    public static void Switch(this string value, Dictionary<string, Action> cases)
    {
        if (cases.ContainsKey(value))
        {
            cases[value]();
        }
    }

    public static Switch<T> Switch<T>(this T value) => new(value);
}

public class Switch<T>
{
    private readonly T _value;
    private bool _caseExecuted;

    public Switch(T value)
    {
        _value = value;
    }

    public Switch<T> CaseWhen(Func<T, bool> condition, Action action)
    {
        if (!_caseExecuted && condition(_value))
        {
            action();
            _caseExecuted = true;
        }

        return this;
    }

    public void Default(Action action)
    {
        if (!_caseExecuted)
        {
            action();
        }
    }
}

public static class Functional
{
    public static void IfElse(bool condition, Action ifTrue, Action ifFalse)
    {
        if (condition)
        {
            ifTrue();
        }
        else
        {
            ifFalse();
        }
    }

    public static T IfElse<T>(bool condition, Func<T> ifTrue, Func<T> ifFalse)
    {
        return condition ? ifTrue() : ifFalse();
    }
}