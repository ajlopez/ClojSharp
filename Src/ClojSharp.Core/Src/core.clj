(def defmacro (mfn [name & fdecl]
	(list 'def name (cons 'mfn fdecl))
))

(defmacro defn [name & fdecl]
	(list 'def name (cons 'fn fdecl))
)
