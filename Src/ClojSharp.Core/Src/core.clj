﻿(def defmacro (mfn [name & fdecl]
	`(def ~name (mfn ~@fdecl))
))

(defmacro defn [name & fdecl]
	`(def ~name (fn ~@fdecl))
)

(defn second [x] (first (next x)))

(defn ffirst [x] (first (first x)))

(defn nfirst [x] (next (first x)))