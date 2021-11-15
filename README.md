# FoundWordInside
This program helps you determine whether a word or a string consists of a few simple words.
If so, the result is similar to this

(in) krankenhaus -> (out) kranken, haus

if not, it leaves the word unchanged

(in) psychology -> (out) psychology.

At the entrance, the program accepts three parameters,

  -path to the dictionary with simple words
  
  -path to the file to record the result
  
  -path to word file to check
  
mandatory of which is only the third.
The result of the work is written to a separate file, the path to which the user specifies, or to the default file "BreakedWord.txt" which is contained in the project folder.
