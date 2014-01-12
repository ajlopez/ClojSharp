# ClojSharp

A Clojure interpreter written in C#, Work in Progress.

## References

- [Reader](http://clojure.org/reader)
- [Special Forms](http://clojure.org/special_forms)
- [Extensible Data Notation](https://github.com/edn-format/edn)

## Notes

- From [Special Forms](http://clojure.org/special_forms)
    * Any metadata on the symbol will be evaluated, and become metadata on the var itself.
    * Many macros expand into def (e.g. defn, defmacro), and thus also convey metadata for the resulting var from the symbol used as the name.
    
## References

- [What does “^:static” do in Clojure?](http://stackoverflow.com/questions/7552632/what-does-static-do-in-clojure)    

## Inception

Rewrite of my previous project [AjSharpure](https://github.com/ajlopez/AjSharpure)




