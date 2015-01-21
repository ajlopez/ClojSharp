(def defmacro (mfn [name & fdecl]
	`(def ~name (mfn ~@fdecl))
))

(defmacro defn [name & fdecl]
	`(def ~name (fn ~@fdecl))
)

(defn second [x] (first (next x)))

(defn ffirst [x] (first (first x)))

(defn nfirst [x] (next (first x)))

(defn fnext [x] (first (next x)))

(defn nnext [x] (next (next x)))

(defn seq? [x] (instance? ClojSharp.Core.Language.ISeq x))

(defn char? [x] (instance? System.Char x))

(defn map? [x] (instance? ClojSharp.Core.Language.Map x))

(defn vector? [x] (instance? ClojSharp.Core.Language.Vector x))

(defn meta [x] 
    (if (instance? ClojSharp.Core.Language.IMetadata x)
	    (. x (Metadata))))

(defn with-meta [x m] 
	(. x (WithMetadata m)))

(defn last [s]
	(if (next s)
		(recur (next s))
		(first s))))

(defn butlast [s]
	(loop [ret [] s s]
		(if (next s)
			(recur (conj ret (first s)) (next s))
            (seq ret))))

(defmacro if-not
	([test then] `(if-not ~test ~then nil))
	([test then else]
		`(if (not ~test) ~then ~else)))

(defmacro when
	[test & body]
	(list 'if test (cons 'do body)))

(defn vector
  ([] [])
  ([a] [a])
  ([a b] [a b])
  ([a b c] [a b c])
  ([a b c d] [a b c d])
  ([a b c d & args]
     (. ClojSharp.Core.Language.Vector (Create (cons a (cons b (cons c (cons d args))))))))

(defn hash-map
	([] {})
	([& keyvals]
		(. ClojSharp.Core.Language.Map (Create (to-array keyvals)))))

(defn assoc
	[map & keyvals]
		(ClojSharp.Core.Language.Map. (to-array keyvals) map))
