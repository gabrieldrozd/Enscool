namespace Common.Utilities.Extensions;

public static class Functional
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

    // TODO: Create method for Pattern Matching (for exampel: x is y)
}