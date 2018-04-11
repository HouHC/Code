#include <stdio.h>
/*
int main()
{
	int i,j;
	printf("\1\1\n");
	for(i=0;i<8;i++)
	{
		for(j=0;j<8;j++)
			if((i+j)%2==0)
				printf("%c%c",219,219);
			else printf("  ");
		printf("\n");
	}
	return 0;
}
*/

//正整数分解质因数 示例14

int main()
{
	int i,n;
	scanf("%d",&n);
	printf("num=");
	while(n!=1)
	{
		for(i=2;i<=n;i++)
		{
			if(n%i==0)
			{
				printf("%d*",i);
				n=n/i;
				break;
			}
		}
	}
	printf("1\n");
	return 0;
}

