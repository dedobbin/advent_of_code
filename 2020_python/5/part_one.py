if __name__ == "__main__":    
    #TODO: finish
    with open("input") as f:
        for line in f.readlines():
            arr1 = [x for x in range(128)]
            arr2 = [x for x in range(8)]

            for val in line:
                if val == "B":
                    #print(arr[:int(len(arr)/2)])
                    arr1 = arr1[:int(len(arr1)/2)]
                elif val == "F":
                    #print(arr[int(len(arr)/2) : len(arr)])
                    arr1 = arr1[int(len(arr1)/2) : len(arr1)]
                elif val == "L":
                    arr2 = arr2[:int(len(arr2)/2)]
                elif val == "R":
                    arr2 = arr2[int(len(arr1)/2) : len(arr2)]
                else:
                    print(val, arr1[0] + arr2[0], "done")