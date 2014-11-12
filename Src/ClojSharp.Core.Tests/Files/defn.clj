(def defn (mfn [name & fdecl]
	(list 'def name (cons 'fn fdecl))
))

(defn incr [x] (+ x 1))

(incr 1)


