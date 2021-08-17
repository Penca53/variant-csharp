# Variant CSharp

C# implementation of C++ [std::variant](https://en.cppreference.com/w/cpp/utility/variant). An instance of Variant at any given time either holds a value of one of its alternative types, or null.

# Usage

## Initialization

`null` (NONE)
```cs
Variant<double, string> explicitConstructorWithNullValue = new Variant<double, string>();
```

`double` (T1)
```cs
Variant<double, string> explicitConstructor = new Variant<double, string>(1d);
Variant<double, string> implicitContructor = 1d;
```

`string` (T2)
```
Variant<double, string> explicitConstructor = new Variant<double, string>("Hello");
Variant<double, string> implicitContructor = "Hello";
```

## Get

T1
```cs
Variant<double, string> variant = 1d;
double value = variant.GetT1();
double value = (double)variant;
```

T2
```cs
Variant<double, string> variant = "Hello";
string value = variant.GetT2();
string value = (string)variant;
```

---

Or use `TryGet` when type isn't known beforehand
```cs
Variant<double, string> variant = 1d;
// T1 = double
if (variant.TryGetT1(out double doubleValue)) // True
{

}
// T2 = string
else if (variant.TryGetT2(out string stringValue)  // False
{

}
// NONE = null
else  // False
{

}
```

## Type Check

`Type`
```cs
Variant<double, string> variant = new Variant<double, string>();
int type = variant.Type; // NONE = 0
```

```cs
Variant<double, string> variant = 1d;
int type = variant.Type; // T1 = 1
```

```cs
Variant<double, string> variant = "Hello";
int type = variant.Type; // T2 = 2
```

`Is<T>`
```cs
Variant<double, string> variant = 1d;
bool isDouble = variant.Is<double>(); // True
bool isString = variant.Is<string>(); // False
```

## Dealing with collections

It may be useful using the `Type` property as index for a collection.

T1
```cs
int[] array = new int[10];
Variant<double, string> variant = 1d;
int index = variant.GetIndex(); // 0
array[index] = 2;
```

T2
```cs
int[] array = new int[10];
Variant<double, string> variant = "Hello";
int index = variant.GetIndex(); // 1
array[index] = 2;
```

NONE (null value)
```cs
int[] array = new int[10];
Variant<double, string> variant = new Variant<double, string>(); // null
int index = variant.GetIndex(); // -1
if (index == -1)
{
  // Variant is empty!
}
```

## Comparison (by value)

```cs
Variant<double, string> variantA = 10d;
Variant<double, string> variantB = 10d;
Variant<double, string> variantC = 17d;

bool areEqual = variantA.Equals(variantB); // True
bool areEqual = variantA.Equals(variantC); // False
```

```cs
Variant<double, string> variantA = "Hello";
Variant<double, string> variantB = "Hello";
Variant<double, string> variantC = "World";

bool areEqual = variantA.Equals(variantB); // True
bool areEqual = variantA.Equals(variantC); // False
```
