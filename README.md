# ClojSharp

A Clojure interpreter written in C#, Work in Progress.

## References

- [Clojure.org](http://clojure.org)
- [Clojure Cheatsheet](http://clojure.org/cheatsheet)
- [Values and Change - Clojure's approach to Identity and State](http://clojure.org/state)
- [Reader](http://clojure.org/reader)
- [Special Forms](http://clojure.org/special_forms)
- [Extensible Data Notation](https://github.com/edn-format/edn)
- [What does “^:static” do in Clojure?](http://stackoverflow.com/questions/7552632/what-does-static-do-in-clojure)    
- [Lazy Sequences in Clojure](http://theatticlight.net/posts/Lazy-Sequences-in-Clojure/)

## Notes

- From [Special Forms](http://clojure.org/special_forms)
    * Any metadata on the symbol will be evaluated, and become metadata on the var itself.
    * Many macros expand into def (e.g. defn, defmacro), and thus also convey metadata for the resulting var from the symbol used as the name.
    
## Inception

Rewrite of my previous project [AjSharpure](https://github.com/ajlopez/AjSharpure)




