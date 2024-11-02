# 目录
[1.变量](#Variable)</br>
[2.资源文件](#ResourceAsset)</br>
[3.属性](#Attribute)</br>

## 变量<a name= "Variable"></a>

### 计时器变量
DecUInt是一个实现了ILifeCycle的变量，它是对UInt的一层封装，并在每一个物理帧自减。
使用构造函数初始化的时候，需要一个GameObject物体作为挂载的对象，
一旦该对象被销毁，DecUInt也将销毁。
</br>
使用样例
<ul>
<li> ```DecUInt foo = new(gameObject);``` </li>

</ul>

</br></br>
DelegateUInt在DecUInt的基础上加入了计时归零触发回调的功能，在使用构造函数初始化的时候指定回调。
同时，DelegateUInt一旦计时不为零，将无法再次设置时间，只能通过Trigger立即触发、Cancel取消触发、
Lengthen延长触发进行更改。
</br></br>

## 资源文件<a name = "ResourceAsset"></a>

### 多重属性资源
MultiAttributeResource是一个继承自ScriptObject的资源文件，它可以包含若干个String、Int、Bool、Float变量。
在运行过程中，只需要对于MAR对象使用GetString方法就能获取指定变量的值。
</br>
在Inspector中只能查看它包含的属性，如果需要编辑，请在MikanLab/多重属性资源编辑器界面中进行。
</br></br>

### 随机表（施工中）
</br></br>


## 属性<a name = "Attribute"></a>

### 静态变量监听
打上[TrackStatic]的类将纳入静态变量监视列表，类中的静态对象可以在KikanLab/静态变量监视器中查看。
</br>
对于静态字段和静态属性，打上[EditableStatic]将显示可编辑的数值，打上[ReadonlyStatic]将显示仅可读的数值。
对于静态方法，只有没有参数的静态方法可以打上[VoidStaticMethod]，此时可以在监听器中通过按钮进行显式调用。
</br></br>