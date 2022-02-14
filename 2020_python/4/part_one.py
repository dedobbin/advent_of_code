

if __name__ == "__main__":

    with open("input") as f:
        #TODO: finish
        res = [a.replace("\n", " ").split(" ") for a in f.read().split("\n\n")]
        print([j for j in [k for k in res]])

 

 
        
        
        #print(f.read().split("\n\n"))
        #res = [i.replace("\n", " ") for i in f.read().split("\n\n")]
        #res = [j.split(" ") for j in [i.replace("\n", " ") for i in f.read().split("\n\n")]]
        
        # [print(k) for k in [j.split(" ") for j in [i.replace("\n", " ") for i in f.read().split("\n\n")]]]

        # print(res)

               