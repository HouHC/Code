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

/*
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
		//printf("a[%d] = %d",i,a[i]);
		num += a[i] * pow(10,len-i-1);//forѭ���������е���ת��Ϊһ������
	}
	return num;
}
//���ַ�������ʽ���룬���ֵļ�⣬��������������ַ���������-1
int MyInputNum(int *arr)
{
	char s[8];
	//int arr[8] = {0};
	int i = 0,num = 0;
	printf("plz input num:");
	scanf("%s",s);
	while(*(s+i) != '\0')
	{
		if((*(s+i)) > '9' || (*(s+i)) < '0')//�ж��Ƿ�������������ַ������򱨴�
		{
			printf("err input!\n");
			return -1;
			//return NULL;
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
		//printf("arr[%d] = %d",i,arr[i]);
		len++,i++;
	}
	//��һ�����ƣ��������ķ�������Ϊint �޴��ݲ���
	//num = MyChange(arr);
	//return num;
	
	return 0;//�ڶ������ƣ���������������Ϊint�� ���ݲ���������Ϊint *
}
//������
int main(void)
{
	//��һ�����ƵĴ���
	//int num = 0;
	//num = MyInputNum();
	//if(num == -1)
	//	return -1;
	int p[8];
	int i;
	int num;
	i = MyInputNum(p);
	if(i == -1)
		return -1;
	for(i=0;i<len;i++)
		printf("p[%d] = %d",i,*(p+i));
	printf("\n");
	num = MyChange(p);
	MyFun(num);
	return 0;
}
*/

//ʾ��27  ��������ַ�����ת������Եݹ����ʽ
/*
void f(int n)
{
	char next;
	if(n<=1)
	{
		next=getchar();
		printf("����෴��˳��\n");
		putchar(next);
	}
	else
	{
		next = getchar();
		f(n-1);
		putchar(next);
	}
} 
int main(void)
{
	int n;
	printf("������5���ַ���\n");
	n = 5;
	f(n);
	puts("");
	return 0;
}
*/
/*
void MyStingFun(const char *s,int n)
{
	if(n == 1)
		printf("%c\n",*s);
	else
	{
		printf("%c",*(s+n-1));//������� ������ַ�����ĸ��ֽ�
		MyStingFun(s,n-1);
	}
}
int main(void)
{
	int n;
	char str[32];
	printf("plz input string:");
	scanf("%s",str);
	n=strlen(str);
	MyStingFun(str,n);
	return 0;
}
*/

//ʾ��32 ɾ���ַ�����ָ������ĸ
/*
char* deleteChar(char* s, char* charSet)
{
	int i;
	int currentIndex = 0;
	int hash[256]= {0};
	for(i = 0;i<strlen(charSet);i++)
	{
		hash[charSet[i]] = 1;
	} 
	
	for(i = 0;i < strlen(s);i++)
	{
		if(hash[s[i]] == 0)
		{
			s[currentIndex++] = s[i];
		}
	} 
	s[currentIndex] = '\0';
	return s;
} 
int main(void)
{
	char s[] = "ffdfdad";
	char charSet[] = "f";
	int i;
	char *t = deleteChar(s,charSet);
	for(i = 0;i<strlen(t);i++)
	{
		printf("%c", t[i]);
	} 
	puts("");
	return 0;
}
*/

char * MydeleteCharSet(char *s,char *charDelete)
{
	int i,j=0,n=0;
	char t[256];
	//printf("s = %s\t%d\n",s,strlen(s));
	//printf("charDelete = %s\t%d\n",charDelete,strlen(charDelete));
	//printf("------\n");
	for(i = 0;i < strlen(s);i++)
	{
		while(j < strlen(charDelete))
		{
			if(s[i] == charDelete[j])
			{
				//printf("s[%d] = %c\n",i,s[i]);
				break;
			}
			j++;
		}
		if(s[i] != charDelete[j])
		{
			t[n++] = s[i];//���ַ����е��ַ���Ҫɾ������ĸ��ͬ�򱣴�
			printf("t[%d] = %c\n",n,t[n-1]);
		}
		j = 0;
	}
	t[n]='\0';
	return t;
}
int main(void)
{
	char s[256];
	char chardelete[26];
	char *t;
	printf("please input a string:");
	scanf("%s",s);
	printf("please input what you want to delete:");
	scanf("%s",chardelete);
	t = MydeleteCharSet(s,chardelete);
	printf("result:%s\n",t);
	return 0;
}

//����ע�ͣ�����git bash �ϴ�
//����ע��
