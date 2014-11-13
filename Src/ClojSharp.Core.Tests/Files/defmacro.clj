(def defmacro (mfn [name & fdecl]
	(list 'def name (cons 'mfn fdecl))
))

(defmacro defn [name & fdecl]
	(list 'def name (cons 'fn fdecl))
)

(defn incr [x] (+ x 1))

(incr 1)


