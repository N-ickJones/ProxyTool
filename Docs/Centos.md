## System Administration Setup & Help

### CentOS - Monitoring
    top
        e - change to human readable
        W - save changes
        h - help

### CentOS -  Add an additional hard disk
    fdisk -l
    fdisk dev/sdb
        n - New Partion Extended...
        w - write changes

    mkfs.ext4 /dev/sdb1

    mount -t ext4 block_device /mount/point
    mount -t ext4 /dev/sdb1 /mount/sdb1

    /etc/fstab     # Permentant change to disk

    systemctl stop docker
    vim /etc/docker/daemon.json
        {
            "data-root": "/mount/sdb1/docker"
        }

    hostnamectl set-hostname desired_name

#### Install Packages
    yum update -y
    yum upgrade -y 
    yum install -y yum-utils dnf dnf-plugins-core
    dnf update -y
    dnf upgrade -y
    dnf install -y epel-release
    dnf install -y nginx
    dnf install -y bind bind-utils
    dnf install -y vim
    dnf install -y git
    dnf install -y certbot certbot-nginx
    dnf config-manager --add-repo https://download.docker.com/linux/centos/docker-ce.repo
    dnf install -y docker-ce docker-ce-cli containerd.io docker-compose

#### Change Docker Disk
    systemctl stop docker
    vim /etc/docker/daemon.json
    {
        "data-root": "/mount/sdb1/docker"
    }
    systemctl daemon-reload
    systemctl restart docker

#### Vim Update
    git clone --depth=1 https://github.com/amix/vimrc.git ~/.vim_runtime
    sh ~/.vim_runtime/install_basic_vimrc.sh
    ssh-keygen
    git@github.com:N-ickJones/ProxyTool.git

#### SSHD config changes
    PermitRootLogin without-password
    PubkeyAuthentication yes
    PasswordAuthentication no   # note: test key login first before this step

#### SSH Key Permissions
    chmod 700 ~/.ssh
    chmod 644 ~/.ssh/authorized_keys
    chmod 644 ~/.ssh/known_hosts
    chmod 644 ~/.ssh/config
    chmod 600 ~/.ssh/id_rsa
    chmod 644 ~/.ssh/id_rsa.pub
    chmod 600 ~/.ssh/github_rsa
    chmod 644 ~/.ssh/github_rsa.pub
    chmod 600 ~/.ssh/mozilla_rsa
    chmod 644 ~/.ssh/mozilla_rsa.pub

#### Not Root User with sudo
    adduser username
    passwd username
    usermod -aG wheel username
    su - username

#### Secure Copy
    ssh -i privKeyFile username@ip_address
    scp local_file remote_username@remote_ip:/remote_directory
    scp remote_username@remote_ip:remote_file /local_directory
    scp -r remote_username@remote_ip:remote_dir /local_dir

#### Useful syntax for file compression
    tar czf name_of_archive_file.tar.gz name_of_directory_to_tar

# Systemd
    cp /opt/www/ProxyTool/systemd/system/* /etc/systemd/system/

# Docker
    docker-compose build
    docker-compose build --no-cache
    docker-compose up
    docker-compose up -d

# Firewalld
    sudo firewall-cmd --zone=public --permanent --add-service=http
    sudo firewall-cmd --zone=public --permanent --add-service=https
    sudo firewall-cmd --zone=public --permanent --add-service=dns

#### SELinux
##### swap -a and -m to add or modify Note: this didnt work
    semanage port -a -t http_port_t -p tcp 8000
    semanage port -a -t http_port_t -p tcp 8001
    semanage port -a -t http_port_t -p tcp 8002
    semanage port -a -t http_port_t -p tcp 8003
    semanage port -a -t http_port_t -p tcp 8004
    semanage port -a -t http_port_t -p tcp 8005
    semanage port -a -t http_port_t -p tcp 8006
    semanage port -a -t http_port_t -p tcp 8007
    semanage port -a -t http_port_t -p tcp 8008

#### Find out if this is secure Note: this works
    sudo semanage permissive -a httpd_t

#### Nginx & Docker Initialization
    systemctl enable nginx
    systemctl start nginx
    systemctl enable docker
    systemctl start docker

#### Bind DNS ( not required )
    vim /etc/named.conf
    firewall-cmd --list-services
    systemctl enable named
    systemctl start named
    vim /etc/hosts
    ls /etc/sysconfig/network-scripts
    cat /etc/resolv.conf -> 127.0.0.1

##### Using Network Manager
    nmcli connection modify enp2s0 +ipv4.addresses "198.204.240.195/29"
    nmcli connection modify enp2s0 +ipv4.addresses "198.204.240.196/29"
    nmcli connection modify enp2s0 +ipv4.addresses "198.204.240.197/29"
    nmcli con mod enp2s0 +ipv4.addresses "198.204.240.198/29"
    ifdown enp2s0 && ifup enp2s0

##### Manually
    vim /etc/sysconfig/network-scripts/ifcfg-enp2s0
    NM_CONTROLLED="no"
    DNS1=127.0.0.1
    GATEWAY=your_gateway
    IPADDR=ip_addr
    PREFIX=29
    IPADDR1=ip_addr
    PREFIX1=29
    IPADDR2=ip_addr
    PREFIX2=29
    IPADDR3=ip_addr
    PREFIX3=29
    IPADDR4=ip_addr
    PREFIX4=29
    ifdown enp2s0 && ifup enp2s0

##### Confirm
    ip addr
    systemctl restart named
    ping google.com
    nslookup google.com
    ###### Troubleshooting: Date might mess this up

##### View DNS Logs
    tail /var/log/messages

##### verify if hostname is neccessary
    vim /etc/sysconfig/network
    NETWORKING=yes
    HOSTNAME=myvenv
    hostname --fqdn

##### Add Domain Name
    vim /etc/sysconfig/network-scripts/ifcfg-enp2s0
    DOMAIN="myvenv.com"
    ifdown enp2s0 && ifup enp2s0
    vim /etc/hosts
    198.204.240.194 myvenv.com
    vim /etc/named.conf
        zone "myvenv.com" IN {
            type master;
            file "myvenv.com.zone";
            allow-update { none; };
        };

        zone "194.240.204.198" IN {
            type master;
            file "myvenv.com.rr.zone";
            allow-update { none; };
        };
    vim /var/named/zone_file
    named-checkconf -z ...
    named-checkzone forward ...
    named-checkzone reverse ...
    chown root:named ...
    named-checkconf -z /etc/named.conf
    named-checkzone forward myvenv.com.zon





### upgrade Centos 7 to Centos 8

#### Repos
	yum install epel-release yum-utils rpmconf -y

#### remove conflicting configurations
	rpmconf -a

#### Cleanup packages
	package-cleanup --leaves
	package-cleanup --orphans

##### remove any above
	yum remove -y 

#### Install dnf
	yum install dnf

#### Uninstall yum
	dnf -y remove yum yum-metadata-parser
	rm -Rf /etc/yum

#### upgrade dnf
	dnf upgrade

#### Install & upgrade centos 8 Note: .rpmnew appended to file conflicts where this file is your settings
	dnf upgrade -y http://mirror.centos.org/centos/8/BaseOS/x86_64/os/Packages/{centos-release-8.1-1.1911.0.8.el8.x86_64.rpm,centos-gpg-keys-8.1-1.1911.0.8.el8.noarch.rpm,centos-repos-8.1-1.1911.0.8.el8.x86_64.rpm}

#### upgrade epel
	dnf upgrade -y epel-release

#### clean up
	dnf clean all

#### remove old kernel
	rpm -e `rpm -q kernel`

#### remove conflicting packages
	rpm -e --nodeps sysvinit-tools

#### launch upgrade 	Note: if python package error remove old package
	dnf -y --releasever=8 --allowerasing --setopt=deltarpm=false distro-sync

#### install new kernel
	dnf -y install kernel-core

#### Install centos minimal 	Note: if python package error remove old package
	dnf -y groupupdate "Core" "Minimal Install"

#### check version
	cat /etc/redhat-release
	
	
#### remove conflicting configurations
	rpmconf -a

