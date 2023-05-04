using System;
using System.Collections.Generic;

// 14/02/2023 pttk & gt:
namespace StringMatch
{
    class Program
    {
        protected async static Task Main(string[] args)
        {
            string s = "ababbab";
            string p = "ab*";
            //int[] m = FindMatch(s, p);
            List<int> m = new List<int>(await FindMatch(s, p));
            Console.WriteLine("Match positions: " + string.Join(", ", m));
        }

        protected async static Task<List<int>> FindMatch(string s, string p)
        {
            List<int> matchPositions = new List<int>();
            var n = s.Length;
            var m = p.Length;
            for (var i = 0; i <= n - m; i++)
            {
                int j;
                for (j = 0; j < m; j++)
                {
                    if (p[j] != '*' && (p[j] != s[i + j]))
                    {
                        break;
                    }
                }
                if (j == m)
                {
                    matchPositions.Add(i);
                }
            }
            return matchPositions.ToList();
        }
    }
}

/* C++ 14 solution:
#include <iostream>
#include <vector>
#include <string>

using namespace std;

// vector-type function:
vector<int> match(const string &S, const string &P) {
  vector<int> M;
  int n = S.length(), m = P.length();
  for (int i = 0; i <= n - m; ++i) {
    bool flag = true;
    for (int j = 0; j < m; ++j) {
      if (P[j] != '*' && P[j] != S[i + j]) {
        flag = false;
        break;
      }
    }
    if (flag) {
      M.push_back(i);
    }
  }
  return M;
}

int main() {
  string S = "ababbab";
  string P = "ab*";
  vector<int> M = match(S, P);
  cout << "Matching positions in S: ";
  for (int i : M) {
    cout << i << " ";
  }
  cout << endl;
  return 0;
}
*/