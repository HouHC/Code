#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>
/*
//ʾ��9
int main(void)
{
	int i,j;
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
/*
//ʾ��11
int main(void)
{
	int i[40],k;
	i[0]=1;
	printf("%d��\n",i[0]);
	i[1]=1;
	printf("%d��\n",i[1]);
	for(k=2;k<40;k++){
		i[k]=i[k-1]+i[k-2];
		printf("%d��\n",i[k]);
	}
	return 0;
}
*/
//ʾ��12
/*
int main(void)
{
	int i,j;
	for(i=101;i<200;i++){
		for(j=2;j<i;j++){
			if(i%j==0)
				break;
			if(j==(i-1))
				printf("%d\n",i);
		}	
	}
	return 0;
}
*/
//ʾ��14 ����

static int len = 0; //ȫ�ֱ���������������ַ������ж�������
//�ֽ��������������޷��ٷֽ�
void MyFun(int n)
{
	int i;
	while(n != 1)
	{
		for(i =  2;i <= n;i++)//
		{
			if(n%i == 0)
			{
				printf("%d*",i);
				n = n/i;
				break;
			}
		}
	}
	printf("1\n");
}
//�������е���ת��Ϊһ������
int MyChange(int *a)
{
	int i,num = 0;
	for(i = 0;i < len;i++){
		printf("a[%d] = %d",i,a[i]);
		num += a[i] * pow(10,len-i-1);//forѭ���������е���ת��Ϊһ������
	}
	return num;
}
//���ַ�������ʽ���룬���ֵļ�⣬��������������ַ���������-1
int MyInputNum(void)
{
	char s[16];
	int arr[16] = {0};
	int i = 0,num = 0;
	printf("plz input num:");
	scanf("%s",s);
	while(*(s+i) != '\0')
	{
		if((*(s+i)) > '9' || (*(s+i)) < '0')//�ж��Ƿ�������������ַ������򱨴�
		{
			printf("err input!\n");
			return -1;
		}
		switch(*(s+i))//switch�жϣ����ַ����ֱ�����������
		{
			case '0':arr[i] = 0;break;
			case '1':arr[i] = 1;break;
			case '2':arr[i] = 2;break;
			case '3':arr[i] = 3;break;
			case '4':arr[i] = 4;break;
			case '5':arr[i] = 5;break;
			case '6':arr[i] = 6;break;
			case '7':arr[i] = 7;break;
			case '8':arr[i] = 8;break;
			case '9':arr[i] = 9;break;
		}
		len++,i++;
	}
	num = MyChange(arr);
	return num;
}
//������
int main(void)
{
	int num = 0;
	num = MyInputNum();
	if(num == -1)
		return -1;
	MyFun(num);
	return 0;
}
