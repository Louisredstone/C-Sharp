using System;
using System.Collections;

namespace Console_Try_1
{

    class Sudoku9x9
    {
        private int[] content;// 0 for undefined, 1~9 for defined.
        private Random rd = new Random();
        public Sudoku9x9(){
            this.content = new int[81];
        }

        public Sudoku9x9 Clone(){
            Sudoku9x9 result=new Sudoku9x9();
            for(int r=0;r<9;r++){
                for(int c=0;c<9;c++){
                    result.set(r,c,this.get(r,c));
                }
            }
            return result;
        }

        public int get(int r,int c){
            return this.content[r*9+c];
        }
        public void set(int r,int c,int v){
            if(v<=9){
                this.content[r*9+c]=v;
            }
            else{
                Console.WriteLine("Error: set value {0} to Sudoku[{1}][{2}], which is out of range [1,9]",v,r,c);
            }
        }

        public void show(){
            for(int ro=0;ro<4;++ro){
                for(int i=0;i<25;i++){
                    Console.Write("-");
                }
                Console.Write("\n");
                if(ro==3) break;
                for(int ri=0;ri<3;++ri){
                    for(int co=0;co<3;co++){
                        Console.Write("| ");
                        for(int ci=0;ci<3;ci++){
                            int r=ro*3+ri;
                            int c=co*3+ci;
                            int v=this.get(r,c);
                            if(v==0) Console.Write("  ");
                            else Console.Write("{0} ",v);
                        }
                    }
                    Console.Write("|\n");
                }
            }
        }

        public int solve(){
            //find the first '0'
            int i=0;
            for(i=0;i<81;i++){
                if(this.content[i]==0) break;
            }
            if (i==81) {
                this.show();
                return 0;//solve successful.-----------------------
            } 
            
            //else, i will be the index of the first empty grid.
            int r=i/9;
            int c=i%9;
            //create the value list(make sure things in it are legal), and create a copy of itself.
            ArrayList valueList = this.getLegalValues(r,c);
            if (valueList.Capacity==0) return 1;//solve failed.-------------

            // Sudoku9x9 selfCopy = new Sudoku9x9();
            foreach(int v in valueList){
                //fill the '0' with a value in the value list
                this.set(r,c,v);
                //a new sudoku was born! try to solve it.
                if(this.solve()==0){
                    //if it works, then it would be the final answer;
                    return 0;
                }
                else{
                    this.set(r,c,0);
                }
                //if not, then try another value in the value list.
            }

            return 1;//solve failed.
        }

        public int solveExh(int ceiling=2,bool show=true){//exhaustive solve
            int cnt=0;
            //find the first '0'
            int i=0;
            for(i=0;i<81;i++){
                if(this.content[i]==0) break;
            }
            if (i==81) {
                if(show){
                    this.show();
                }
                return 1;
                //solve successful, 1 possibility found.-----------------------
            } 
            
            //else, i will be the index of the first empty grid.
            int r=i/9;
            int c=i%9;
            //create the value list(make sure things in it are legal), and create a copy of itself.
            ArrayList valueList = this.getLegalValues(r,c);
            if (valueList.Capacity==0) return 0;//solve failed.-------------

            // Sudoku9x9 selfCopy = new Sudoku9x9();
            foreach(int v in valueList){
                //fill the '0' with a value in the value list
                this.set(r,c,v);
                //a new sudoku was born! try to solve it.
                //different from solve()
                cnt+=this.solveExh();
                if (cnt>=ceiling) return cnt;
                this.set(r,c,0);
            }

            return cnt;//solve done.
        }

        // public int solveExh2(int ceiling=2){

        //     return 0;
        // }

        public void generate(){
            this.clear();
            //randomSort
            ArrayList listEmpty=new ArrayList();
            ArrayList listOccup=new ArrayList();
            for(int i=0;i<81;i++){
                int r=i/9;
                int c=i%9;
                if(this.get(r,c)!=0) listEmpty.Add(i);
                else listOccup.Add(i);
            }
            int index=listOccup.Capacity;
            ArrayList randomList=randomSort(listOccup,this.rd);
            foreach(int item in randomSort(listEmpty,this.rd)){
                randomList.Add(item);
            }
            
            bool flag=true;
            while(flag){
                int r=index/9;
                int c=index%9;
                foreach(int item in randomSort(this.getLegalValues(r,c),this.rd)){
                    bool flag2=false;
                    this.set(r,c,item);
                    switch(this.solveExh(show: false)){
                        case 0://conflicting restricts
                        break;
                        case 1://just enough restricts, or could be less. Prune.
                        break;
                        default://not enough restricts.
                            index++;
                            flag2=true;
                        break;
                    }
                    if(flag2) break;
                }
            }
        }

        public ArrayList getLegalValues(int r,int c){
            ArrayList valueList = new ArrayList();
            int ii,rr,cc;
            bool flag=true;
            for(ii=1;ii<=9;ii++){
                flag=true;
                for(rr=0;rr<9;rr++){
                    // if (rr==r) continue;
                    if (this.get(rr,c)==ii) {flag=false;break;}
                }
                for(cc=0;cc<9;cc++){
                    // if (cc==c) continue;
                    if (this.get(r,cc)==ii) {flag=false;break;}
                }
                for(int rp=0;rp<3;rp++){
                    for(int cp=0;cp<3;cp++){
                        // if ((rp==r%3)&&cp==c%3) continue;
                        rr=rp+(r/3)*3;
                        cc=cp+(c/3)*3;
                        if (this.get(rr,cc)==ii) {flag=false;break;}
                    }
                }
                if(flag) valueList.Add(ii);
            }
            return valueList;
        }
        
        public void clear(){
            for(int i=0;i<81;i++){
                this.content[i]=0;
            }
        }


        public void shuffle(){//just for test.
            for(int i=0;i<81;i++){
                this.content[i]=this.rd.Next(1,10);
            }
        }

        public void sample(){//just for test.
            int[] tmp={   0,0,6,0,0,0,0,0,1,
                            0,7,0,0,6,0,0,5,0,
                            8,0,0,1,0,3,2,0,0,
                            0,0,5,0,4,0,8,0,0,
                            0,4,0,7,0,2,0,9,0,
                            0,0,8,0,1,0,7,0,0,
                            0,0,1,2,0,5,0,0,3,
                            0,6,0,0,7,0,0,8,0,
                            2,0,0,0,0,0,4,0,0};
            this.content=(int [])tmp.Clone();
        }

        ArrayList randomSort(ArrayList input, Random rd){
            ArrayList result=new ArrayList();
            foreach(var item in input){
                result.Insert(rd.Next(0,result.Capacity),item);
            }
            return result;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            Sudoku9x9 sudoku = new Sudoku9x9();
            sudoku.sample();
            sudoku.show();
            if(sudoku.solveExh()>1) Console.WriteLine("Sudoku Error.");
            sudoku.show();
        }
    }
}
