# BUSANBIMSLIB
  �λ걤���� ���������ý��� .NET�� ���̺귯�� 

## ����
**BusanBimsLib**�� [�λ걤���� ���������ý��� API](https://www.data.go.kr/data/15092750/openapi.do)�� .NET���� ���ϰ� �� �� �ִ� Wrapper ���̺귯���Դϴ�.


## �ٿ�ε� &amp; ��ġ



## �����
**BusanBimsLib**�� ��� �۾��� `BusanBimsLib.BusanBimsClient` Ŭ�������� �����մϴ�.

### API Ŭ���̾�Ʈ �����ϱ�
API Ŭ���̾�Ʈ �ν��Ͻ�(`BusanBimsClient`)�� ������ ��, [��������������](https://data.go.kr/)���� �߱޹��� ����Ű �� __Decoding__ ����Ű�� ����Ͽ��� �մϴ�.
(Encoding ����Ű�� ����� ��� ����Ű�� ����� �ν����� ���� ���� �ֽ��ϴ�.)

* C# (>= 9.0, >= .NET 6)
```csharp
using BusanBimsLib;

BusanBimsClient bis = new("DECODING_ACCESS_KEY"); // <= �������������п��� �߱޹��� Decoding ����Ű ���

...

```

### ���������� �˻��ϱ�

```csharp
using BusanBimsLib;

BusanBimsClient bis = new("DECODING_ACCESS_KEY");

BusStopListResponseData busStopList = await bis.GetBusStopList("����");

Console.WriteLine($"�˻����: {busStopList.Count}��\n");

foreach(var item in busStopList)
{
	Console.WriteLine($"���������� �̸�: {item.BusStopName}");
	Console.WriteLine($"���������� ID  : {item.BusStopID}");
	Console.WriteLine($"���������� ����: {item.BusStopKind}");
	Console.WriteLine($"���������� ��ġ: {item.Location}");
	Console.WriteLine();
}
```

��� ���
```
�˻����: 12��

���������� �̸�: ���鿪.�Ե�ȣ�ڹ�ȭ��
���������� ID  : 164630302
���������� ����: �Ϲ�
���������� ��ġ: 35.157735, 129.055546

���������� �̸�: ���麹����
���������� ID  : 164650201
���������� ����: �Ϲ�
���������� ��ġ: 35.153564, 129.057776

......

���������� �̸�: ���鱳����
���������� ID  : 506980000
���������� ����: �Ϲ�
���������� ��ġ: 35.157888797896, 129.061751007201

```

### �����뼱 �˻��ϱ�

```csharp
using BusanBims;

BusanBimsClient bis = new("DECODING_ACCESS_KEY");

BusInfoResponseData busInfo = await bis.GetBusInfo("80");


```


## ���̼���
**BusanBimsLib**�� [GNU LGPL 2.1 ���̼���](https://www.olis.or.kr/license/Detailselect.do?lId=1005)�� ���� �����Ӱ� �̿�, ����, ����, ������� �����մϴ�.
���� �� ������ϴ� ��� �ҽ��ڵ� ���� ��û�� �ݵ�� ���Ͽ��� �ϸ�, ������ ���̼����� �����Ͽ� �����Ͽ��� �մϴ�.
�����ڴ� �� ���̺귯�� �� �ҽ��ڵ带 ��������ν� �߻��ϴ� ��ü�� ������ ���Ͽ� å������ �ʽ��ϴ�.

## �⿩
