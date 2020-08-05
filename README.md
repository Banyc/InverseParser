# Inverse Parser

Generate/compose text following a given grammar.

## Build

```
dotnet run
```

## Grammar Definition

Two kind of files to define a grammar, namely grammatical definition file with extension `gg` and lexical definition file with extension `gl`.

Those files could be placed anywhere as long as the path is inside the folder `GrammarDefinitionReader.RootDirectory`. Besides, `gg` files or `gl` files could be many.

### Rule

- Any text following `#` and before the nearest newline will be commented out
- A line started with `#` will be considered as a line with no char.

### Grammatical Definition

The definition is written is BNF(Backus–Naur form)-like form.

#### Rule

- Allow arbitrary new blank lines.
- Tokens should be divided by space.
- The first token of a line is the left-hand side of the derivation.
    - the remaining tokens is the right-hand side of the derivation.
- Allow self referring.
- Allow same left-hand-side tokens in many lines. It represents all possible choice of derivations.
- If the token is not defined in the lexical definition file, it is considered as a literal token/terminal.

#### Example

```text
# grammatical.gg

S A S
A B C
A B
```

...The derivation process start from `S`. the third line indicates that `S` will derivate `A S`. And the next line indicates that `A` will derivate `B C`. While the next line also indicates that `A` might also derivate `B`.

### Lexical Definition

#### Rule

- blank lines to partition different non-terminals.
- A line started with `#` is also considered as a blank line.

#### Example

```text
# lexical.gl

B
bbb
BBB

C
CCC
ccc
```

...It indicates that the non-terminal `B` could be derivated into terminals `bbb` or `BBB`. It is the same to `C`.

### Result

With the example above, the results might be:

```
BBBbbbccc
```
