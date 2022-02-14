if __name__ == "__main__":    
    with open("input") as f:
        #TODO: finish
        for group in (f.read() + "\n").split("\n\n"):
            print("-----new list-------")
            yes = [False] * 26
            for answer in group:
                print(answer)
                if not answer.strip():
                    print ("Group done", yes)
                    exit()
                #print(answer, ord(answer)-97)
                yes[ord(answer)-97] = True
                #print("setting ", ord(answer)-97, yes)