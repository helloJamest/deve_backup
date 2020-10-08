import base64
import os

img_dir = r'./img'
count = 1
for img in sorted(os.listdir(img_dir)):
    with open(img_dir+r'/'+img, 'rb') as f:
        base64_data = base64.b64encode(f.read())
        s = base64_data.decode()
        #print('img',img)
        print('[base64str'+str(count)+']:data:image/png;base64,%s' % s)
        count += 1

