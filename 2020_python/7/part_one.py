canContainShinyGold = False

def can_contain(bag):
    print(bag)
    if bag == "light red":
        res = {"bright white":1, "muted yellow":2}
    elif bag == "dark orange":
        res = {"bright white":3,"muted yellow":4}
    elif bag == "bright white":
        res = {"shiny gold":1}
    elif bag == "muted yellow":
        res = {"shiny gold":2, "faded blue":9}
    elif bag == "shiny gold":
        res = {"dark olive": 1, "vibrant plum":2}
    elif bag == "dark olive":
        res = {"faded blue":3, "dotted black":4}
    elif bag == "vibrant plum":
        res = {"faded blue":5, "dotted black":6}
    elif bag == "faded blue":
        res = {}
    elif bag == "dotted black":
        res = {}
    
    if res == {}:
        canContainShinyGold = False;
        return

    for key in res.keys():
        for i in range(res[key]):
            can_contain(key)

        

if __name__ == "__main__":    
    res = can_contain('vibrant plum')
    

    #with open("input") as f: