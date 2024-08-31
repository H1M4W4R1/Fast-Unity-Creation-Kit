# Priority

Priority is a simple package that allows to sort objects in lists by their priority. 
It can be either used to create a `PrioritizedList<TObject>` of objects which is automatically
sorted when a new object is "added" (aka. inserted) or to use a custom extension method
`SortByPriority<TObject>` provided in `PriorityExtensions` class to sort any `IList<TObject>` 
by priority.

All `TObject` should inherit from `IPrioritySupport` interface which provides a `Priority` property.

<note>
The priority is sorted in ascending order, so the lower the priority, the closer to the beginning of the list
the object will be.

<b>For example:</b> object A with priority 3 will be always before object B with priority 5. If both objects have the same priority,
the order of insertion is unknown and the order of objects is not guaranteed.

In case of PrioritizedList, the order of insertion allows to somewhat predict the order of objects with the same priority -
objects will always be sorted by the inverse order of insertion - the first inserted object will be at the end of the list.
You can consider this similar to a reverse-order sorting.
</note>

## Interfaces
### IPrioritySupport
This interface should be implemented by all objects that should be sorted by priority.

## Example usage
Example prioritized object:
```C#
public class PrioritizedObjectA : IPrioritySupport
{
    public uint Priority => 3;
    
     public int CompareTo(IPrioritySupport other) => Priority.CompareTo(other.Priority);
}

public class PrioritizedObjectB : IPrioritySupport
{
    public uint Priority => 5;
    
     public int CompareTo(IPrioritySupport other) => Priority.CompareTo(other.Priority);
}
```

Example usage of `PrioritizedList<TObject>`:
```C#
var list = new PrioritizedList<IPrioritySupport>();
list.Add(new PrioritizedObjectB());
list.Add(new PrioritizedObjectA());

// list[0] is PrioritizedObjectA
// list[1] is PrioritizedObjectB
```

Of course, you can use the extension method `SortByPriority<TObject>` on any `IList<TObject>`:
```C#
var list = new List<IPrioritySupport>();
list.Add(new PrioritizedObjectB());
list.Add(new PrioritizedObjectA());

list.SortByPriority();

// list[0] is PrioritizedObjectA
// list[1] is PrioritizedObjectB
```

<note>
It is strongly recommended to avoid using SortByPriority on the list as it is not as efficient as PrioritizedList.
</note>