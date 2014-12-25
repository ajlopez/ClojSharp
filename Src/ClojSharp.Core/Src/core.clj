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
